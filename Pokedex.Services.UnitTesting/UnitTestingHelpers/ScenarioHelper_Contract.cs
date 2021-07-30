using AutoMapper;
using Pokedex.Services.Contract;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class ScenarioHelper_Contract
    {
        public static PokemonBasic CreatePokemonBasic(string name, string description, PokemonHabitat? pokemonHabitat = null, bool? isLegendary = null)
        {  
            return new PokemonBasic
            {
                Name = name,
                Description = description,
                Habitat = pokemonHabitat ?? PokemonHabitat.Cave,
                IsLegendary = isLegendary ?? true
            };
        }
    }
}
