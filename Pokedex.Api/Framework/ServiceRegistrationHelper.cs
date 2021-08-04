using Microsoft.Extensions.DependencyInjection;
using Pokedex.Services;

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
            
            //Register Pokedex service module (this way until we move to gRPC/http)
            services.RegisterPokedexServiceModule();
        }        
    }
}
