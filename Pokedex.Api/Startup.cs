using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Api.Framework;

namespace Pokedex.Api
{
    public class Startup
    {   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Pokedex.Services.DataContractMappingProfile), typeof(Framework.ServicesMappingProfile));

            //We've extracted this to an extension method on the Framework namespace to keep the plumbing out of Startup
            // and keep this class readable
            services.RegisterPokedexServices();

            services.AddControllers();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute("Default",
                    "/api/{controller}/{pokemonName?}",
                    new { controller = "Pokemon", action = "Index" });
            });
        }
    }
}
