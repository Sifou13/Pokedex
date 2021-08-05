using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Pokedex.Api.Clients;
using Pokedex.Api.Controllers;
using Pokedex.Api.Framework;

namespace Pokedex.Api.UnitTesting.UnitTestingHelpers
{
    public class UnitTestSetupHelper
    {
        public static PokemonController CreatePokemonController(Mock<IPokemonInformationServiceClient> pokemonInformationServiceClient, Mock<ICacheService> cacheService)
        {   
            return new PokemonController(pokemonInformationServiceClient.Object, cacheService.Object);
        }

        public static TranslatedPokemonController CreateTranslatedPokemonController(Mock<IPokemonInformationServiceClient> pokemonInformationServiceClient, Mock<ICacheService> cacheService)
        {
            return new TranslatedPokemonController(pokemonInformationServiceClient.Object, cacheService.Object);
        }
    }
}
