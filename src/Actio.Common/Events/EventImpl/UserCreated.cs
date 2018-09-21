﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events.EventImpl
{
   public class UserCreated:IEvent
    {
        public string Email { get; }
        public string Name { get; }

        protected UserCreated()
        {
        }

        public UserCreated(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
