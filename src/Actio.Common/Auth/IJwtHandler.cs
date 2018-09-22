using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Auth
{
   public interface IJwtHandler
    {
        JSonWebToken Create(Guid userId);
    }
}
