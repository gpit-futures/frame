using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Futures.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futures.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IContainer _applicationContainer;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            this._applicationContainer = Bootstrapper.SetupContainer(services);
            return new AutofacServiceProvider(this._applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                this._applicationContainer.Dispose();
            });
        }
    }
}
