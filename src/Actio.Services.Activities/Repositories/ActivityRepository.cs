using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _dabase;
        public ActivityRepository(IMongoDatabase database)
        {
            _dabase = database;
        }

        public async Task AddAsync(Activity activity)
        {
            await collections.InsertOneAsync(activity);
        }

        public async Task<Activity> GetAsync(Guid Id)
        {
            var activity = await collections.AsQueryable().FirstOrDefaultAsync(c => c.Id == Id);
            return activity;
        }

        private IMongoCollection<Activity> collections
            => _dabase.GetCollection<Activity>("Activities");
    }
}
