using System;
using System.Runtime.Serialization.Formatters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Futures.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
                //ioc.AddTransient(c => new JsonSerializer
                //{
                //    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                //    Formatting = Formatting.None,
                //    CheckAdditionalContent = true,
                //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //    ObjectCreationHandling = ObjectCreationHandling.Auto,
                //    DefaultValueHandling = DefaultValueHandling.Ignore,
                //    TypeNameHandling = TypeNameHandling.None,
                //    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                //    MissingMemberHandling = MissingMemberHandling.Ignore,
                //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //    NullValueHandling = NullValueHandling.Ignore
                //});
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
