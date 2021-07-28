namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class ScenarioHelper_ResourceDataAccess
    {
        public static Services.Resources.DataAccess.Pokemon.PokemonRoot CreatePokemonBasic(string name, string species = null, string url = null)
        {  
            return new Services.Resources.DataAccess.Pokemon.PokemonRoot
            {
                Name = name,
                Species = new Services.Resources.DataAccess.Pokemon.PokemonSpeciesLink
                {
                    Name = species ?? "Pokemon",
                    Url = url ?? "Specific Species URL"
                }                
            };
        }
    }
}
