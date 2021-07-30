using Microsoft.Extensions.DependencyInjection;
using Pokedex.Services;
using Pokedex.Services.Contract;
using Pokedex.Services.Contract.Orchestrators;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon;
using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;

namespace Pokedex.Api.Framework
{
    public static class ServiceRegistrationHelper
	{
		public static void RegisterPokedexServices(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            //These two dependencies could be extracted to a framework project as it will be relevant to more than just this Api project (csproj)
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICacheService, CacheService>();

            services.RegisterConfig();
            services.RegisterPokedexResources();
            services.RegisterPokedexOrchestrators();
        }

        //We would not do this for a production service, we should in fact be usind a service (Possibly gRPC since WCF has been left out of .Net Core),
        //which I have not used yet) using publicly exposed contract and not the service's implementations - Generally via proxy
        //Keeping this in production would defeat the way the solution was designed
        #region Pokedex Service
        private static void RegisterConfig(this IServiceCollection services)
        {
            services.AddSingleton<IConfig, Config>();
        }

        private static void RegisterPokedexResources(this IServiceCollection services)
        {
            services
                .AddScoped<IPokemonDA, PokemonDA>()
                .AddScoped<ITranslator, Translator>()
                .AddScoped<ITranslationResource, TranslationResource>()
                .AddScoped<IPokemonResource, PokemonResource>();
        }

        private static void RegisterPokedexOrchestrators(this IServiceCollection services)
        {
            services.AddScoped<IPokemonInformationOrchestrator, PokemonInformationOrchestrator>();
        }
        #endregion
    }
}
