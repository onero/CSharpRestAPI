using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAppBLL;
using RestAppBLL.BusinessObjects;

namespace CustomerRestAPI
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var bllFacade = new BLLFacade();
                bllFacade.CustomerService.Create(
                    new CustomerBO
                    {
                        FirstName = "Adamino",
                        LastName = "Hansen",
                        Address = "Home"
                    });

                bllFacade.OrderService.Create(new OrderBO()
                {
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddMonths(1)
                });
            }

            app.UseMvc();
        }
    }
}