using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NotificationService.DataAccess.DTO;
using NotificationService.DataAccess.Repositories;
using NotificationService.Domain;
using NotificationService.MailServices;
using NotificationService.Models;

namespace NotificationService.Services
{
    public class SendingService
    {
        private readonly IncomingMessagesRepository _repository;
        private readonly BettorAddressRepository _bettorAddressRepository;
        private readonly IMailService _mailService;

        private readonly ILogger<SendingService> _logger;
        private readonly IMapper _mapper;

        private readonly int _processedCount;

        public SendingService(IncomingMessagesRepository incomingMessagesRepository
            , BettorAddressRepository bettorAddressRepository
            , IMapper mapper
            , ILogger<SendingService> logger
            , IMailService mailService
            , IConfiguration config)
        {
            _repository = incomingMessagesRepository;
            _logger = logger;
            _mapper = mapper;
            _mailService = mailService;

            _bettorAddressRepository = bettorAddressRepository;

            try
            {
                _processedCount = int.Parse(config.GetSection("MessagesProcessedCount").Value);
            }
            catch (Exception ex)
            {
                _processedCount = 1;
                _logger.LogError(ex, "[SendingService] Messages will be processed one at time.");
            }
        }

        public async Task SendMessagesAsync()
        {
            try
            {
                var messagesToSend = _mapper.Map<List<MessageForSending>>(await _repository.GetNextMessagesAsync(_processedCount));
                if (messagesToSend == null || !messagesToSend.Any())
                {
                    _logger.LogInformation("[SendingService][SendMessagesAsync] There are no messages to send.");
                    return;
                }

                await _repository.UpdateStatesAsync(new UpdateMessageStatesRequest()
                {
                    Ids = messagesToSend.Select(x => x.Id),
                    State = SendingStates.InSendingProcess
                });

                var sentSuccessfully = new List<Guid>(messagesToSend.Count);
                var sentUnSuccessfully = new List<Guid>();

                foreach (var message in messagesToSend)
                {
                    var address = await GetAddress(message, sentUnSuccessfully);
                    if (!string.IsNullOrEmpty(address))
                    {
                        await SendMessageAsync(message, new List<string>([address]), sentSuccessfully, sentUnSuccessfully);
                    }
                }

                if (sentSuccessfully.Any())
                {
                    _logger.LogInformation($"[SendingService][SendMessagesAsync] {sentSuccessfully.Count} messages have been sent successfully.");
                    await _repository.UpdateStatesAsync(new UpdateMessageStatesRequest()
                    {
                        Ids = sentSuccessfully,
                        State = SendingStates.Successfully
                    });
                }

                if (sentUnSuccessfully.Any())
                {
                    _logger.LogInformation($"[SendingService][SendMessagesAsync] {sentUnSuccessfully.Count} messages could not be sent.");
                    await _repository.UpdateStatesAsync(new UpdateMessageStatesRequest()
                    {
                        Ids = sentUnSuccessfully,
                        State = SendingStates.Failure
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SendingService][SendMessagesAsync] Message sending failed.");
            }
        }

        private async Task SendMessageAsync(MessageForSending message, List<string> addresses, List<Guid> sentSuccessfully, List<Guid> sentUnSuccessfully)
        {
            try
            {
                var data = new MailData(addresses, message.Subject, message.Message);

                var result = await _mailService.SendAsync(data);
                if (result.IsSending)
                {
                    sentSuccessfully.Add(message.Id);
                    _logger.LogInformation($"[SendingService][SendMessageAsync] Message '{message.Id}' sent successfully.");
                }
                else
                {
                    _logger.LogError($"[SendingService][SendMessageAsync] Message '{message.Id}' was not sent.{Environment.NewLine}{result.Error}");
                    sentUnSuccessfully.Add(message.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[SendingService][SendMessageAsync] Message '{message.Id}' was not sent.");
                sentUnSuccessfully.Add(message.Id);
            }
        }

        private async Task<string> GetAddress(MessageForSending message, List<Guid> sentUnSuccessfully)
        {
            var address = await _bettorAddressRepository.GetByBettorIdWithMinPriorityAsync(message.TargetId);
            if (address == null)
            {
                _logger.LogError($"[SendingService][GetAddress] Message '{message.Id}' was not sent because address to send was not found.");
                sentUnSuccessfully.Add(message.Id);
                return string.Empty;
            }
            return address.Address;
        }
    }
}
