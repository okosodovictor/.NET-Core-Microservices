using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Name { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; set; }

        protected User()
        {

        }

        public User(string email, string name)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ActioException("Empty user email",
                    $"User email can not be empty");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ActioException("Empty user name",
                    $"User name can not be empty");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = Name;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncripter encriptor)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ActioException("Empty user password",
                    $"User password can not be empty");
            }

            Salt = encriptor.GetSalt();
            password = encriptor.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncripter encriptor)
            => password.Equals(encriptor.GetHash(password, Salt));
    }
}
