﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events.EventImpl;
using Actio.Common.Services.ServiceImpl;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Actio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                      .UseRabbitMq()
                      .SubScribeToEvent<ActivityCreated>()
                      .Build()
                      .Run();
                                     
        }
    }
}
