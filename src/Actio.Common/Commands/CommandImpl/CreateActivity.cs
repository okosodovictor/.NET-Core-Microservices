﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Commands
{
    public class CreateActivity : IAuthenticationCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string  Category { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
