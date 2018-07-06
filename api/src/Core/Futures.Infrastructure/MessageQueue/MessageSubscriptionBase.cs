using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Futures.Infrastructure.MessageQueue
{
    public abstract class MessageSubscriptionBase
    {
        public IEnumerable<Type> GetHandlersFromAssembly(Type type)
        {
            var handlers =
                from x in Assembly.GetAssembly(type).GetTypes()
                from z in x.GetInterfaces()
                where z.IsGenericType && typeof(IMessageHandler<>).IsAssignableFrom(z.GetGenericTypeDefinition())
                select x;


            return handlers;
        }
    }
}