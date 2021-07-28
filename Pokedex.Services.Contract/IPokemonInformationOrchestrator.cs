
using System.Threading.Tasks;

namespace Pokedex.Services.Contract
{
    public interface IPokemonInformationOrchestrator
    {
        Task<PokemonBasic> GetPokemonDetailsAsync(string pokemonName);

        PokemonBasic GetTranslatedPokemonDetails(string name);
    }
}