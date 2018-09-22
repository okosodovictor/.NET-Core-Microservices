using Actio.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _dabase;
        public ActivityRepository(IMongoDatabase database)
        {
            _dabase = database;
        }

        public async Task AddAsync(Activity model)
        {
            await collections.InsertOneAsync(model);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            var activities = await collections.AsQueryable().Where(x=>x.UserId==userId).ToListAsync();
            return activities;
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            var activity = await collections.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);
            return activity;
        }

        private IMongoCollection<Activity> collections
          => _dabase.GetCollection<Activity>("Activities");
    }
}
