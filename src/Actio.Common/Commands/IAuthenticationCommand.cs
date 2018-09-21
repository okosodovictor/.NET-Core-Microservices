using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Commands
{
   public interface IAuthenticationCommand:ICommand
    {
        Guid UserId { get; set; }
    }
}
