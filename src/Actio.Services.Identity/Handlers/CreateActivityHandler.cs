using Actio.Common.Commands;
using Actio.Common.Events.EventImpl;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

         public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity{command.Name}");
            await _busClient.PublishAsync(new ActivityCreated(command.UserId, command.Id, command.Name, command.Category, command.Description, DateTime.UtcNow));
        }
    }
}
