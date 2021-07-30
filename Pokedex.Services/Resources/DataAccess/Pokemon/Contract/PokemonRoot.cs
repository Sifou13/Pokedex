namespace Pokedex.Services.Resources.DataAccess.Pokemon.Contract
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
