using System.Text.Json.Serialization;
using EncryptionApp.ApiGateway.Application.HttpClients;
using EncryptionApp.ApiGateway.Infrastructure.Attributes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptionApp.ApiGateway.Infrastructure.Extensions
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
        
        public static IServiceCollection AddEncryptionServiceApiClient(this IServiceCollection services)
        {
            services
                .AddHttpClient(ServerClients.EncryptionService);

            services.AddTransient<IEncryptionServiceHttpClient, EncryptionServiceHttpClient>();

            return services;
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