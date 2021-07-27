﻿using AutoMapper;
using Pokedex.Services.Contract;

namespace Pokedex.Services.UnitTesting
{
    public static class ScenarioHelper_Contract
    {
        public static PokemonBasic CreatePokemonBasic(string name, string description, PokemonHabitat? pokemonHabitat = null, bool? IsLegendary = null)
        {  
            return new PokemonBasic
            {
                Name = name,
                Description = description,
                PokemonHabitat = pokemonHabitat ?? PokemonHabitat.Cave,
                IsLegendary = IsLegendary ?? true
            };
        }
    }
}
