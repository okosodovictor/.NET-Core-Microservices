using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{

    public class MongoSeeder : IDatabaseSeeder
    {

        protected readonly IMongoDatabase _database;

        public MongoSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCusor = await _database.ListCollectionsAsync();
            var collections = await collectionCusor.ToListAsync();
            if (collections.Any())
            {
                return;
            }

            await CustomSeederAsync();

        }

        protected virtual async Task CustomSeederAsync()
        {
            await Task.CompletedTask;
        }
    }
}
