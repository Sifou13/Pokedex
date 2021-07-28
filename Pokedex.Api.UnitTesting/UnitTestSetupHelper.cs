using AutoMapper;
using Moq;
using Pokedex.Api.Controllers;
using Pokedex.Services.Contract;

namespace Pokedex.Api.UnitTesting
{
    public class UnitTestSetupHelper
    {
        public static PokemonController CreatePokemonController(Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator)
        {
            IMapper mapper = CreateMapper();

            return new PokemonController(pokemonInformationOrchestrator.Object, mapper);
        }

        private static IMapper CreateMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<Framework.ServicesMappingProfile>());

            return new Mapper(config);
        }
    }
}
