using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.IOC
{
    public class DependancyContainer
    {

        public static void RegisterServices(IServiceCollection services, string connectionString)
        {

            //when are the instances created?
            // Scoped - IOC contatiner will create an instance of the specified service type once per request and will be shared in a single request (1 request -> 1 instance)
            // Transient - the IOC container will create a new instance of the specified service tpe everyrime you ask for it (1 request -> multiple instances)
            // Singleton - IOC container will create and share a single instance of a service throughout he application's lifetime (many requests -> 1 instance)


            services.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseSqlServer(connectionString)
                );


            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductService>();

            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();
        }

    }
}
