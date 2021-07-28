namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class ScenarioHelper_ResourceContract
    {
        public static Services.Resources.Contract.PokemonBasic CreatePokemonBasic(string name, string description, Services.Resources.Contract.PokemonHabitat? pokemonHabitat = null, bool IsLegendary = true)
        {  
            return new Services.Resources.Contract.PokemonBasic
            {
                Name = name,
                Description = description,
                Habitat = pokemonHabitat ?? Services.Resources.Contract.PokemonHabitat.Cave,
                IsLegendary = IsLegendary 
            };
        }
    }
}
