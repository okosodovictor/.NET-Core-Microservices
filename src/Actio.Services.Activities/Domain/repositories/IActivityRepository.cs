using Actio.Services.Activities.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Domain.repositories
{
   public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid Id);
        Task AddAsync(Activity activity);
    }
}
