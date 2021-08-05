using Microsoft.Extensions.DependencyInjection;
using Pokedex.Api.Clients;

namespace Pokedex.Api.Framework
{
    public static class ServiceRegistrationHelper
	{
		public static void RegisterPokedexApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            //These two dependencies could be extracted to a framework project as it will be relevant to more than just this Api project (csproj)
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICacheService, CacheService>();


            services.AddSingleton<IPokemonInformationServiceClient, PokemonInformationServiceClient>();
        }        
    }
}
