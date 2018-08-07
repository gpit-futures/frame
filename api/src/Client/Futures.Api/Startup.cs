using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Futures.Application.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RawRabbit;
using RawRabbit.Attributes;
using RawRabbit.Common;
using RawRabbit.vNext;

namespace Futures.Api
{
    public class Startup
    {
        private IContainer _applicationContainer;

        private IBusClient _bus;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

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

            services.AddRawRabbit(config => config.AddJsonFile("rabbitmq.json"),
                ioc => { ioc.AddSingleton<IConfigurationEvaluator, AttributeConfigEvaluator>(); });

            var token = this.Configuration.GetSection("Jwk").Get<JsonWebKey>();

            services.AddAuthorization(conf =>
            {
                conf.AddPolicy("Read", pol => pol.RequireClaim("authorities", "FOO_READ"));
                conf.AddPolicy("Write", pol => pol.RequireClaim("authorities", "FOO_WRITE"));
            })
            .AddAuthentication(conf =>
            {
                conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(conf =>
            {
                conf.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = token
                };
            });

            this._applicationContainer = Bootstrapper.SetupContainer(services);
            this._bus = Bootstrapper.SetupMessageSubscriptions(services, this._applicationContainer);
            return new AutofacServiceProvider(this._applicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy");
            app.UseJwtSignalRAuthentication();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSignalR(config => { config.MapHub<NotificationsHub>("/ws/notifications"); });

            lifetime.ApplicationStopped.Register(() =>
            {
                this._bus.ShutdownAsync();
                this._applicationContainer.Dispose();
            });
        }
    }
}