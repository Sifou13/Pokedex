using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Contract
{
    public class PokemonBasic
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public PokemonHabitat Habitat { get; set; }

        public bool IsLegendary { get; set; }
    }

    public enum PokemonHabitat
    {
        None = 0,
        Cave = 1,
        Forest = 2,
        Grassland = 3,
        Mountain = 4,
        Rare = 5,
        RoughTerrain = 6,
        Sea = 7,
        Urban = 8,
        WatersEdge = 9
    }
}
