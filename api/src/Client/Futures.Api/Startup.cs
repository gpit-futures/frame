using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Futures.Infrastructure.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Attributes;
using RawRabbit.Common;
using RawRabbit.vNext;

namespace Futures.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IContainer _applicationContainer;

        private IBusClient _bus;

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(config => config.AddPolicy("CorsPolicy", x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
                x.AllowCredentials();
            }));
            services.AddMvc();
            services.AddSignalR();

            services.AddRawRabbit(config => config.AddJsonFile("rabbitmq.json"), ioc =>
            {
                ioc.AddSingleton<IConfigurationEvaluator, AttributeConfigEvaluator>();
            });

            this._applicationContainer = Bootstrapper.SetupContainer(services);
            this._bus = Bootstrapper.SetupMessageSubscriptions(services, this._applicationContainer);
            return new AutofacServiceProvider(this._applicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseSignalR(config =>
            {
                config.MapHub<NotificationsHub>("/ws/notifications");
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                this._bus.ShutdownAsync();
                this._applicationContainer.Dispose();
            });
        }
    }
}
