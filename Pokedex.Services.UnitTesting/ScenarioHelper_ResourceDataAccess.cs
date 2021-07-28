namespace Pokedex.Services.UnitTesting
{
    public static class ScenarioHelper_ResourceDataAccess
    {
        public static Services.Resources.DataAccess.PokemonBasic CreatePokemonBasic(string name, string description, Services.Resources.DataAccess.PokemonHabitat? pokemonHabitat = null, bool? IsLegendary = null)
        {  
            return new Services.Resources.DataAccess.PokemonBasic
            {
                Name = name,
                Description = description,
                PokemonHabitat = pokemonHabitat ?? Services.Resources.DataAccess.PokemonHabitat.Cave,
                IsLegendary = IsLegendary ?? true
            };
        }
    }
}
