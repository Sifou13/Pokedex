namespace Pokedex.Api.UnitTesting.UnitTestingHelpers
{
    public class ScenarioHelper_ApiModels
    {
        public static Models.PokemonBasic CreatePokemon(string name, string description, Models.PokemonHabitat habitat = Models.PokemonHabitat.Cave, bool isLegendary = false)
        {
            return new Models.PokemonBasic
            {
                Name = name,
                Description = description,
                Habitat = habitat,
                IsLegendary = isLegendary
            };
        }
    }
}
