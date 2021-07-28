using System.Text.Json.Serialization;

namespace Pokedex.Services.Resources.DataAccess.Pokemon
{
    public class PokemonRoot
    {
        public string Name { get; set; }

        public PokemonSpeciesLink Species { get; set; }           
    }

    public class PokemonSpeciesLink : INamedAPIResource
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
