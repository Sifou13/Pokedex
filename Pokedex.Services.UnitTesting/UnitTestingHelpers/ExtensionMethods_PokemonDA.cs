using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokedex.Services.Resources.DataAccess.Pokemon;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class ExtensionMethods_PokemonDA
    {
        public static PokemonSpecies AddFlavorTextEntry(this PokemonSpecies pokemonSpecies, string flavorText, string language)
        {
            List<FlavorTextEntry> flavorTextEntries = pokemonSpecies.Flavor_Text_Entries?.ToList() ?? new List<FlavorTextEntry>();

            flavorTextEntries.Add(new FlavorTextEntry
            {
                Flavor_Text = flavorText,
                Language = new Language
                {
                    Name = language ?? "en"
                }
            });

            pokemonSpecies.Flavor_Text_Entries = flavorTextEntries.ToArray();

            return pokemonSpecies;
        }
    }
}
