using AutoMapper;
using Moq;
using Pokedex.Api.Controllers;
using Pokedex.Api.Framework;
using Pokedex.Services.Contract.Orchestrators;

namespace Pokedex.Api.UnitTesting.UnitTestingHelpers
{
    public class UnitTestSetupHelper
    {
        public static PokemonController CreatePokemonController(Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator)
        {
            IMapper mapper = CreateMapper();

            return new PokemonController(pokemonInformationOrchestrator.Object, mapper);
        }

        public static TranslatedPokemonController CreateTranslatedPokemonController(Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator)
        {
            IMapper mapper = CreateMapper();

            return new TranslatedPokemonController(pokemonInformationOrchestrator.Object, mapper);
        }

        private static IMapper CreateMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<ServicesMappingProfile>());

            return new Mapper(config);
        }
    }
}
