using Actio.Common.Commands;
using Actio.Common.Commands.CommandImpl;
using Actio.Common.Events.EventImpl;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _service;
        private readonly ILogger _logger;
        public CreateUserHandler(IBusClient busClient, IUserService service, ILogger<CreateUserHandler> logger)
        {
            _busClient = busClient;
            _service = service;
            _logger = logger;
        }

         public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: {command.Email} {command.Name}");
            try
            {
                await _service.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email,command.Name));

                return;
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, "Error occured", ex.Message));
                _logger.LogError(ex.Message);
            }

            await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
        }
    }
}
