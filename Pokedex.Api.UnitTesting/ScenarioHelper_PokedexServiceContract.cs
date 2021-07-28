using Pokedex.Services.Contract;

namespace Pokedex.Api.UnitTesting
{
    public static class ScenarioHelper_PokedexServiceContract
    {
        public static PokemonBasic CreatePokemonBasic(string name, string description, PokemonHabitat pokemonHabitat = PokemonHabitat.Cave, bool IsLegendary = false)
        {
            return new Pokedex.Services.Contract.PokemonBasic
            {
                Name = name,
                Description = description,
                PokemonHabitat = pokemonHabitat,
                IsLegendary = IsLegendary
            };
        }
    }
}
