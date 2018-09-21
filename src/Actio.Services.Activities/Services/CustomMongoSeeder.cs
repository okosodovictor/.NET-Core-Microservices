using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder: MongoSeeder
    {

        private readonly ICategoryRepository _repo;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository repository)
          :base(database)
        {
            _repo = repository;
        }

        protected override async Task CustomSeederAsync()
        {
            var categories = new List<string>
            {
                "Work",
                "Sport",
                "hobby"
            };

           await Task.WhenAll(categories.Select(x => _repo.AddAsync(new Category(x))));
        }
    }
}
