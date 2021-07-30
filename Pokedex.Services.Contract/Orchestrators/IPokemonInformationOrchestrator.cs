using System.Threading.Tasks;

namespace Pokedex.Services.Contract.Orchestrators
{
    public interface IPokemonInformationOrchestrator
    {
        Task<PokemonBasic> GetPokemonDetailsAsync(string pokemonName);

        Task<PokemonBasic> GetTranslatedPokemonDetailsAsync(string name);
    }
}