using MassTransit;
using UserServer.Core.DTO;

namespace UserServer.WebHost.RabitService
{
    public class UserCreatedConsumer : IConsumer<ResponceUserDto>
    {
        public Task Consume(ConsumeContext<ResponceUserDto> context)
        {
            var message = context.Message;
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
