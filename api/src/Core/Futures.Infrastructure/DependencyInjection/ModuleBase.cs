using System;
using System.Collections.Generic;
using Autofac;

namespace Futures.Infrastructure.DependencyInjection
{
    public abstract class ModuleBase : Autofac.Module
    {
        private List<Func<Type, bool>> IgnoreWhere { get; } = new List<Func<Type, bool>>();

        protected void PreventAutoRegistrationWhere(Func<Type, bool> ignoreWhere)
        {
            this.IgnoreWhere.Add(ignoreWhere);
        }

        protected override void Load(ContainerBuilder builder)
        {
            this.Register(builder);
        }

        protected void RegisterSingleton<TClass, TInterface>(ContainerBuilder builder)
        {
            builder
                .RegisterType<TClass>()
                .As<TInterface>()
                .SingleInstance();

            this.PreventAutoRegistrationWhere(t => t == typeof(TClass));
        }

        protected abstract void Register(ContainerBuilder builder);
    }
}