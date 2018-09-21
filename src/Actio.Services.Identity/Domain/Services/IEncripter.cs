﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncripter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
