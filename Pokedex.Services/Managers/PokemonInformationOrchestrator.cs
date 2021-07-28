using AutoMapper;
using Pokedex.Services.Contract;
using Pokedex.Services.Resources.Contract;
using System;
using System.Threading.Tasks;

namespace Pokedex.Services.Orchestrators
{
    public class PokemonInformationOrchestrator : IPokemonInformationOrchestrator
    {
        private readonly IPokemonResource pokemonResource;
        private readonly IMapper mapper;

        public PokemonInformationOrchestrator(IPokemonResource pokemonResource, IMapper mapper)
        {
            this.pokemonResource = pokemonResource;
            this.mapper = mapper;
        }

        public async Task<Contract.PokemonBasic> GetPokemonDetailsAsync(string pokemonName)
        {
            Resources.Contract.PokemonBasic retrievedPokemonBasic = pokemonResource.SelectByName(pokemonName);

            return await Task.FromResult(mapper.Map<Contract.PokemonBasic>(retrievedPokemonBasic));
        }

        public Contract.PokemonBasic GetTranslatedPokemonDetails(string name)
        {
            throw new NotImplementedException();
        }
    }
}
