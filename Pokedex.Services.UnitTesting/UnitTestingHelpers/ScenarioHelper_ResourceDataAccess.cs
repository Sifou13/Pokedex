using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class ScenarioHelper_ResourceDataAccess
    {
        public static PokemonRoot CreatePokemonBasic(string name, string species = null, string url = null)
        {  
            return new PokemonRoot
            {
                Name = name,
                Species = new PokemonSpeciesLink
                {
                    Name = species ?? "Pokemon",
                    Url = url ?? "Specific Species URL"
                }                
            };
        }
    }
}
