using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
   public interface IActivityService
    {
        Task AddAsync(Guid id, Guid UserId, string Category, string name, string description, DateTime createdAt);
    }
}
