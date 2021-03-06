using Pokedex.Services.Contract;

namespace Pokedex.Api.UnitTesting.UnitTestingHelpers
{
    public static class ScenarioHelper_PokedexServiceContract
    {
        public static PokemonBasic CreatePokemonBasic(string name, string description, PokemonHabitat Habitat = PokemonHabitat.Cave, bool IsLegendary = false)
        {
            return new Pokedex.Services.Contract.PokemonBasic
            {
                Name = name,
                Description = description,
                Habitat = Habitat,
                IsLegendary = IsLegendary
            };
        }
    }
}
