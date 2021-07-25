
namespace Pokedex.Services.Contract
{
    public interface IPokemonInformationOrchestrator
    {
        PokemonBasic GetPokemonDetails(string pokemonName);

        PokemonBasic GetTranslatedPokemonDetails(string name);
    }
}