using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Api.Framework;
using Pokedex.Services.Contract.Orchestrators;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting.UnitTestingHelpers
{
    public static class MockHelper
    {
        #region Pokedex Service
        public static void ExpectGetPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Pokedex.Services.Contract.PokemonBasic pokemonToReturn)
        {
            pokemonInformationOrchestrator
                .Setup(x => x.GetPokemonDetailsAsync(name))
                .Returns(Task.FromResult(pokemonToReturn))
                .Verifiable();
        }

        public static void VerifyGetPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Times? callCount = null)
        {
            pokemonInformationOrchestrator.Verify(x => x.GetPokemonDetailsAsync(name), callCount ?? Times.AtMostOnce());
        }

        public static void ExpectGetTranslatedPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Pokedex.Services.Contract.PokemonBasic pokemonToReturn)
        {
            pokemonInformationOrchestrator
                .Setup(x => x.GetTranslatedPokemonDetailsAsync(name))
                .Returns(Task.FromResult(pokemonToReturn))
                .Verifiable();
        }

        public static void VerifyGetTranslatedPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Times? callCount = null)
        {
            pokemonInformationOrchestrator.Verify(x => x.GetTranslatedPokemonDetailsAsync(name), callCount ?? Times.AtMostOnce());
        }
        #endregion

        #region Memory Cache

        public static void ExpectTryGetValue(this Mock<ICacheService> cacheService, string cacheKey, Models.PokemonBasic pokemonBasicCacheValueToReturn)
        {
            cacheService
                .Setup(x => x.TryGetValue<Models.PokemonBasic>(cacheKey))
                .Returns(pokemonBasicCacheValueToReturn)
                .Verifiable();
        }

        public static void ExpectSet(this Mock<ICacheService> cacheService, string cacheKey, Models.PokemonBasic pokemonBasicCacheValueToReturn)
        {
            cacheService
                .Setup(x => x.Set(cacheKey, It.Is<Models.PokemonBasic>(cacheValue => AssertCacheValueProperties(pokemonBasicCacheValueToReturn, cacheValue))))
                .Verifiable();
        }

        public static void VerifyTryGetValue(this Mock<ICacheService> cacheService, string cacheKey, Times? callCount = null)
        {
            cacheService.Verify(x => x.TryGetValue<Models.PokemonBasic>(cacheKey), callCount ?? Times.AtMostOnce());
        }

        public static void VerifySet(this Mock<ICacheService> cacheService, string cacheKey, Models.PokemonBasic pokemonBasicCacheValueToReturn, Times? callCount = null)
        {
            cacheService.Verify(x => x.Set(cacheKey, pokemonBasicCacheValueToReturn), callCount ?? Times.AtMostOnce());
        }

        private static bool AssertCacheValueProperties(Models.PokemonBasic expectedCacheValue, Models.PokemonBasic actualCacheValue)
        {
            if (expectedCacheValue == null && actualCacheValue == null)
                return true;

            Assert.AreEqual(expectedCacheValue.Name, actualCacheValue.Name);
            Assert.AreEqual(expectedCacheValue.Description, actualCacheValue.Description);
            Assert.AreEqual(expectedCacheValue.Habitat, actualCacheValue.Habitat);
            Assert.AreEqual(expectedCacheValue.IsLegendary, actualCacheValue.IsLegendary);

            return true;
        }
        #endregion
    }
}
