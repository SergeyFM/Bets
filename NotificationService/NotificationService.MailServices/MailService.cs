﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace NotificationService.MailServices
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<SendingMessageResult> SendAsync(MailData mailData)
        {
            try
            {
                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);

                // Receiver
                foreach (string mailAddress in mailData.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if (!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                if (mailData.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                // CC
                // Check if a CC address was supplied in the request
                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content
                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();
                #endregion

                #region Send Mail
                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(_settings.UserName, _settings.Password);
                        await client.SendAsync(mail);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                        client.Dispose();
                    }
                }
                #endregion

                return SendingMessageResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return SendingMessageResult.CreateFailure(ex);
            }
        }
    }
}
