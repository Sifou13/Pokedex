using Microsoft.Extensions.DependencyInjection;
using Pokedex.Services;
using Pokedex.Services.Contract;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon;

namespace Pokedex.Api.Framework
{
    public static class ServiceRegistrationHelper
	{
		public static void RegisterPokedexServices(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            //We would not do this for a production service, we should in fact be usind a service (Possibly gRPC since WCF has been left out of .Net Core,
            //which I have not used yet) using publicly exposed contract and not the full dll - Generally via proxy
            //Keeping this in production would defeat the way the solution was designed
            
            services.RegisterConfig();
            services.RegisterPokedexResources();
            services.RegisterPokedexOrchestrators();
        }

        private static void RegisterConfig(this IServiceCollection services)
        {
            services.AddScoped<IConfig, Config>();
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
