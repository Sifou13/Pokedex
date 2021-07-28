using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services.Resources.DataAccess.Pokemon
{
    public interface IPokemonDA
    {
        Task<PokemonRoot> SelectByName(string name);

        Task<PokemonSpecies> SelectSpeciesByUrl(string url);
    }

    public class PokemonDA : IPokemonDA
    {
        private readonly string PokemonRelativePath = "pokemon/";

        private readonly IConfig config;

        public PokemonDA(IConfig config)
        {
            this.config = config;
        }

        public async Task<PokemonRoot> SelectByName(string name)
        {
            string requestedPokemonApiUrl = $"{config.PokemonAPIUrl}{PokemonRelativePath}{name.ToLower()}";

            return await Infrastructure.WebRequestHelper.GetResponse<PokemonRoot>(requestedPokemonApiUrl, CreateJsonOptions());
        }

        public async Task<PokemonSpecies> SelectSpeciesByUrl(string url)
        {
            return await Infrastructure.WebRequestHelper.GetResponse<PokemonSpecies>(url, CreateJsonOptions());
        }

        private JsonSerializerOptions CreateJsonOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new PokemonHabitatEnumJsonConverter() } };
        }
    }
}
