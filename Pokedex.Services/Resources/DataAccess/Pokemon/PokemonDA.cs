using Pokedex.Infrastructure.DataAccess;
using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services.Resources.DataAccess.Pokemon
{    
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

            return await WebRequestHelper.Get<PokemonRoot>(requestedPokemonApiUrl, jsonSerializerOptions: CreateJsonOptions());
        }

        public async Task<PokemonSpecies> SelectSpeciesByUrl(string url)
        {
            return await WebRequestHelper.Get<PokemonSpecies>(url, jsonSerializerOptions: CreateJsonOptions());
        }

        private JsonSerializerOptions CreateJsonOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new PokemonHabitatEnumJsonConverter() } };
        }
    }
}
