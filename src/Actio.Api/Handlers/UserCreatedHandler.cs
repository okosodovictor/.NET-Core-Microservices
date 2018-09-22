using Actio.Api.Repositories;
using Actio.Common.Events;
using Actio.Common.Events.EventImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {

        private readonly IActivityRepository _activityRepository;

        public UserCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine($"User created: {@event.Name}");
        }
    }
}
