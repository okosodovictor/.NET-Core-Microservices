using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncripter _encripter;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IEncripter encripter,
            IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encripter = encripter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ActioException("email_in_use",
                    $"email: '{email}' Is already in use");
            }

            user = new User(email, name);
            user.SetPassword(password, _encripter);
            await _userRepository.AddAsync(user);
        }

        public async Task<JSonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
            {
                throw new ActioException("Invalid_credentials",
                    $"Invalid credentials.");
            }

            if (!user.ValidatePassword(password, _encripter))
            {
                throw new ActioException("Invalid_credentials",
                    $"Invalid credentials.");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}
