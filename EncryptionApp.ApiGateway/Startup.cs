using EncryptionApp.ApiGateway.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EncryptionApp.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersCustom();
            services.AddHealthChecks();
            services.AddResponseCompression();
            services.AddEncryptionServiceApiClient();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EncryptionApp.EncryptionService", Version = "v1" });
            });
            
            services.AddForwardedHeadersOptions();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs"); });
            }
            
            app.UseHttpsRedirection();
            app.UseErrorHandler(env);
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(builder =>
            {
                builder.MapHealthChecks("/api/ping");
                
                builder
                    .MapDefaultControllerRoute()
                    .RequireAuthorization();

                builder
                    .MapControllers()
                    .RequireAuthorization();
            });
        }
    }
}