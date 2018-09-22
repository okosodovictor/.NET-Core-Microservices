using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using Actio.Common.Events.EventImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity
            {
                Id = @event.Id,
                Name = @event.Name,
                Category = @event.Category,
                CreateAt = @event.CreatedAt,
                Description = @event.Description,
                UserId = @event.UserId
            });

            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
