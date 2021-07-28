using AutoMapper;
using Pokedex.Services.Contract;
using Pokedex.Services.Resources.Contract;
using System;
using System.Threading.Tasks;

//To explain this layer - Using the IDesign Methodology, this layer called Orchestrator/Manager namespace
// will contain the main business logic behind the service, and will orchestrate a number of calls to
// different resources (if this became the requirement) while handling the use case
// this allow also for greater testability of the whole flow, mocking only third parties that have overhead
// to be tested in isolation, so that the "CRUD" is only tested once
//The Mapping (AutoMapper) is being tested as part of the flow, and this is a technical decision, or a trade-off when using IDesign
// but it does actually test that developpers have added the correct mapping the profile maps.
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
