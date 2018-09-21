using Actio.Common.Commands;
using Actio.Common.Events.EventImpl;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler: ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _service;
        private ILogger _logger;
        public CreateActivityHandler(IBusClient busClient, IActivityService service, ILogger logger)
        {
            _busClient = busClient;
            _service = service;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating activity{command.Name}");
            try
            {
                await _service.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                await _busClient.PublishAsync(new ActivityCreated(command.UserId, command.Id, command.Name, command.Category, command.Description, DateTime.UtcNow));

                return;
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, command.UserId, command.Name, ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, command.UserId, command.Name, "Error occured", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}
