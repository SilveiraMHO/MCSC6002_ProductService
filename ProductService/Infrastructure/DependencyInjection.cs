using Application.Interfaces.Messaging;
using Application.Interfaces.Repositories;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure.Context;
using Infrastructure.Messaging;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDatabase(config);

            services.AddRabbitMQServiceConfiguration();

            services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IBrandService, BrandService>()
                .AddScoped<IImageService, ImageService>()

                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IBrandRepository, BrandRepository>()
                .AddScoped<IImageRepository, ImageRepository>()

                .AddScoped<IEventPublisher, EventPublisher>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsHistoryTable("Migrations", "EFCore");
                })
            );
        }

        private static void AddRabbitMQServiceConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, mqConfig) =>
                {
                    mqConfig.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                });
            });
        }
    }
}
