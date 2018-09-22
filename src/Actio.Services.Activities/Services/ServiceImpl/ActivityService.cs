using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
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
            var activityCategory = await _categoryRepository.GetAsync(Category);
            if (activityCategory == null)
            {
               throw new ActioException("category_not_found",
                   $"Category: {Category} was not found");
            }

            var activity = new Activity(id, name, Category, description, UserId, createdAt);

            await _activityRepository.AddAsync(activity);
        }
    }
}
