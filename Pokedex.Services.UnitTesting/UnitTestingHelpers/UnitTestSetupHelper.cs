using AutoMapper;
using Moq;
using Pokedex.Services.Contract;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources.Contract;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class UnitTestSetupHelper
    {
        public static IPokemonInformationOrchestrator CreatePokemonInformationOrchestratorService(Mock<IPokemonResource> pokemonResource)
        {
            IMapper mapper = CreateMapper();

            return new PokemonInformationOrchestrator(pokemonResource.Object, mapper);
        }

        public static IMapper CreateMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<DataContractMappingProfile>());
            
            return new Mapper(config);
        }
    }
}
