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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _dabase;
        public CategoryRepository(IMongoDatabase database)
        {
            _dabase = database;
        }

        public async Task<Category> GetAsync(string name)
        {
           var category = await collections.AsQueryable().FirstOrDefaultAsync(c=>c.Name ==name.ToLowerInvariant());
            return category;
        }

        public async Task AddAsync(Category category)
        {
            await collections.InsertOneAsync(category);
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
        {
            var categories = await collections.AsQueryable().ToListAsync();
            return categories;
        }

        private IMongoCollection<Category> collections
            => _dabase.GetCollection<Category>("Categories");
    }
}
