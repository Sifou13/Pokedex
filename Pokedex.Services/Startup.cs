using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Pokedex.Services
{
    public class Startup
    {   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddAutoMapper(typeof(Pokedex.Services.DataContractMappingProfile));

            services.RegisterPokedexServiceModule();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseEndpoints(config =>
            {
                config.MapGrpcService<Orchestrators.PokemonInformationOrchestrator_GrpcService>();

                config.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client.");
                });
            });
        }
    }
}
