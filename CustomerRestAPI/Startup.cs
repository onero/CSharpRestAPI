using System;
using System.Collections.Generic;
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
            services.AddCors(o => o.AddPolicy("LocalPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //var bllFacade = new BLLFacade();

                //var address = bllFacade.AddressService.Create(
                //    new AddressBO()
                //    {
                //        City = "Esbjerg",
                //        Street = "Home",
                //        Number = "1"
                //    });

                //var address2 = bllFacade.AddressService.Create(
                //    new AddressBO()
                //    {
                //        City = "Køge",
                //        Street = "OldHome",
                //        Number = "1"
                //    });

                //var customer = bllFacade.CustomerService.Create(
                //    new CustomerBO
                //    {
                //        FirstName = "Adamino",
                //        LastName = "Hansen",
                //        AddressIds = new List<int>() { address.Id, address2.Id }
                //    });

                //bllFacade.OrderService.Create(new OrderBO()
                //{
                //    OrderDate = DateTime.Now,
                //    DeliveryDate = DateTime.Now.AddMonths(1),
                //    CustomerId = customer.Id
                //});
            }

            app.UseMvc();
        }
    }
}