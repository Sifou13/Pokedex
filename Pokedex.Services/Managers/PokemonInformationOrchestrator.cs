using Pokedex.Services.Contract;
using Pokedex.Services.Resources.Contract;
using System;

namespace Pokedex.Services.Orchestrators
{
    public class PokemonInformationOrchestrator : IPokemonInformationOrchestrator
    {
        private readonly IPokemonResource pokemonResource;

        public PokemonInformationOrchestrator(IPokemonResource pokemonResource)
        {
            this.pokemonResource = pokemonResource;
        }

        public Contract.PokemonBasic GetPokemonDetails(string pokemonName)
        {
            throw new NotImplementedException();
        }

        public Contract.PokemonBasic GetTranslatedPokemonDetails(string name)
        {
            throw new NotImplementedException();
        }
    }
}
