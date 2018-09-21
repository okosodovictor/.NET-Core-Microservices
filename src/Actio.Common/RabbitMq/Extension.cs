using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using RawRabbit.Pipe.Middleware;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.RabbitMq
{
    public static class Extension
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        => bus.SubscribeAsync<TCommand>(async message => await handler.HandleAsync(message),
                context => context.UseConsumerConfiguration(cfg =>
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));


        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
           IEventHandler<TEvent> handler) where TEvent : IEvent
       => bus.SubscribeAsync<TEvent>(async message => await handler.HandleAsync(message),
               context => context.UseConsumerConfiguration(cfg =>
               cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
           var section =  configuration.GetSection("RabbitMq");
            section.Bind(options);

            //create our client
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options,         
            });

            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}
