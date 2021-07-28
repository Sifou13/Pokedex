namespace Pokedex.Services.Resources.DataAccess.Pokemon
{
    public class PokemonSpecies
    {
        public Habitat Habitat { get; set; }

        public bool Is_Legendary { get; set; }

        public FlavorTextEntry[] Flavor_Text_Entries { get; set; }
    }

    public class Habitat
    {
        public PokemonHabitat Name { get; set; }
    }

    public class FlavorTextEntry
    {
        public string Flavor_Text { get; set; }

        public Language Language { get; set; }
    }

    public class Language
    {
        public string Name { get; set; }
    }
}
