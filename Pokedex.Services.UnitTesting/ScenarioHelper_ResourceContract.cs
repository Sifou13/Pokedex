namespace Pokedex.Services.UnitTesting
{
    public static class ScenarioHelper_ResourceContract
    {
        public static Services.Resources.Contract.PokemonBasic CreatePokemonBasic(string name, string description, Services.Resources.Contract.PokemonHabitat? pokemonHabitat = null, bool? IsLegendary = null)
        {  
            return new Services.Resources.Contract.PokemonBasic
            {
                Name = name,
                Description = description,
                PokemonHabitat = pokemonHabitat ?? Services.Resources.Contract.PokemonHabitat.Cave,
                IsLegendary = IsLegendary ?? true
            };
        }
    }
}
