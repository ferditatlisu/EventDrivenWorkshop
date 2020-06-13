using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit.AspNetCoreIntegration;
using MassTransit.RabbitMqTransport;
using MassTransit;
using EDCommon.RabbitMQ;
using EDCommon;
using PulseIntegrationService.Consumers;

namespace PulseIntegrationService
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
            //services.AddHealthChecks();
            services.AddControllers();
            //INFO : Need to install this package MassTransit.AspNetCore from NuGet
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PriceOrderConsumer>();
                x.AddConsumer<PlaceOrderConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    //config.UseHealthCheck(context);
                    config.Host("rabbitmq", "/", host =>
                    {
                        host.Username("test");
                        host.Password("test");
                    });
                    config.ReceiveEndpoint(CustomKey.RABBITMQ_PRICE_ORDER_REQUEST_ENDPOINT, ep =>
                    {
                        ep.ConfigureConsumer<PriceOrderConsumer>(context);
                    });
                    config.ReceiveEndpoint(CustomKey.RABBITMQ_PLACE_ORDER_REQUEST_ENDPOINT, ep =>
                    {
                        ep.ConfigureConsumer<PlaceOrderConsumer>(context);
                    });
                }));
            });

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
            });
        }
    }
}
