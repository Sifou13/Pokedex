namespace Pokedex.Api.UnitTesting
{
    public class ScenarioHelper_ApiModels
    {
        public static Models.PokemonBasic CreatePokemon(string name, string description, Models.PokemonHabitat habitat = Models.PokemonHabitat.Cave, bool isLegendary = false)
        {
            return new Models.PokemonBasic
            {
                Name = name,
                Description = description,
                PokemonHabitat = habitat,
                IsLegendary = isLegendary
            };
        }
    }
}
