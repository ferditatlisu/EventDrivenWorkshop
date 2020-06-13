using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BFF.Consumers;
using BFF.Hubs;
using EDCommon;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BFF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderResponseConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    //config.UseHealthCheck(context);
                    config.Host("rabbitmq", "/", host =>
                    {
                        host.Username("test");
                        host.Password("test");
                    });
                    config.ReceiveEndpoint(CustomKey.RABBITMQ_ORDER_RESPONSE_ENDPOINT, ep =>
                    {
                        ep.ConfigureConsumer<OrderResponseConsumer>(context);
                    });
                }));
            });

            services.AddSingleton<OrderHub>();
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderHub>("/orderHub");
            });
        }
    }
}
