using System.Reflection;
using System.Text.Json.Serialization;
using EncryptionApp.EncryptionService.Application.Services;
using EncryptionApp.EncryptionService.Infrastructure.Attributes;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptionApp.EncryptionService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddControllersCustom(this IServiceCollection services)
        {
            services
                .AddControllers(options => { options.Filters.Add<ModelValidationAttribute>(); })
                .AddNewtonsoftJson()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddFluentValidation(fvc =>
                {
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IAesKeyService, AesKeyService>();
            return services;
        }
        
        public static IServiceCollection AddMediatorCustom(this IServiceCollection services)
        {
            services
                .AddRequestValidationBehavior()
                .AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            return services;
        }
        
        public static IServiceCollection AddRequestValidationBehavior(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            return services.AddFluentValidation(assemblies);
        }
        
        public static IServiceCollection AddForwardedHeadersOptions(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            return services;
        }
    }
}