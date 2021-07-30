using AutoMapper;
using Moq;
using Pokedex.Services.Contract.Orchestrators;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources.Contract;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class UnitTestSetupHelper
    {
        public static IPokemonInformationOrchestrator CreatePokemonInformationOrchestratorService(Mock<IPokemonResource> pokemonResource, Mock<ITranslationResource> translationResource = null)
        {
            IMapper mapper = CreateMapper();

            return new PokemonInformationOrchestrator(pokemonResource.Object, translationResource?.Object, mapper);
        }

        private static IMapper CreateMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<DataContractMappingProfile>());
            
            return new Mapper(config);
        }
    }
}
