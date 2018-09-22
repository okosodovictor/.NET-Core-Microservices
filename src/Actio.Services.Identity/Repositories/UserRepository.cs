using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _dabase;
        public UserRepository(IMongoDatabase database)
        {
            _dabase = database;
        }

        public async Task AddAsync(User user)
        {
            await collections.InsertOneAsync(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await collections.AsQueryable().FirstOrDefaultAsync(u=>u.Id==id);
            return user;
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await collections.AsQueryable().FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());
            return user;
        }

        private IMongoCollection<User> collections
         => _dabase.GetCollection<User>("Users");
    }
}
