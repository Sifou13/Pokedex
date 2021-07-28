using Microsoft.Extensions.DependencyInjection;
using Pokedex.Services.Contract;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess;

namespace Pokedex.Api.Framework
{
    public static class ServiceRegistrationHelper
	{
		public static void RegisterPokedexServices(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            //We would not do this for a production service, we should in fact be usind a service
            //and only consuming and using publicly exposed contract and not the full dll - Generally via proxy
            //Keeping this in production would defeat the way the solution was designed

            services.RegisterPokedexResources();
            services.RegisterPokedexOrchestrators();
        }

        private static void RegisterPokedexResources(this IServiceCollection services)
        {
            services.AddScoped<IPokemonDA, PokemonDA>();
            services.AddScoped<IPokemonResource, PokemonResource>();
        }

        private static void RegisterPokedexOrchestrators(this IServiceCollection services)
        {
            services.AddScoped<IPokemonInformationOrchestrator, PokemonInformationOrchestrator>();
        }
    }
}
