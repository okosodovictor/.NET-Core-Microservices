using Actio.Services.Activities.Domain.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services.ServiceImpl
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid UserId, string Category, string name, string description, DateTime createdAt)
        {
            throw new NotImplementedException();
        }
    }
}
