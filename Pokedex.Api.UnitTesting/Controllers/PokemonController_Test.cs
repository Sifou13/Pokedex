using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Api.Controllers;
using Pokedex.Api.Framework;
using Pokedex.Api.UnitTesting.UnitTestingHelpers;
using Pokedex.Services.Contract.Orchestrators;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting.Controllers
{
    //This was created as a standalone test class since this controller seem like it  be used as the CRUD for Pokemons as we should keep
    // those tests in isolation from other that relate to manipulation of the Pokemon

    [TestClass]
    public class PokemonController_Test
    {
        private PokemonController pokemonController;
        private Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestratorMock;
        private Mock<ICacheService> cacheServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            cacheServiceMock = new Mock<ICacheService>(MockBehavior.Strict);
            pokemonInformationOrchestratorMock = new Mock<IPokemonInformationOrchestrator>(MockBehavior.Strict);

            pokemonController = UnitTestSetupHelper.CreatePokemonController(pokemonInformationOrchestratorMock, cacheServiceMock);
        }

        [TestCleanup]
        public void Cleanup()
        {
            pokemonInformationOrchestratorMock.VerifyAll();
        }

        [TestMethod]
        public async Task Index_DefaultApiEndpoint_ShouldNotBeHit_ProvideRelevantDocumentationToUserForCorrectUseOfApi()
        {
            string expectedPokemonControllerDefaultString = @"You need to choose a pokemon name, and add it to the URL: currentURL + ""/pikachu";

            ActionResult<string> result = await pokemonController.Index();

            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());

            Assert.AreEqual(expectedPokemonControllerDefaultString, (result.Result as OkObjectResult).Value);
        }

        [TestMethod]
        public async Task Get_Pikachu_NotCached_GetsPokemonFromService_AddedToCacheAndExpectedPokemonReturned()
        {
            string pokemonName = "Pikachu";

            Pokedex.Services.Contract.PokemonBasic fakeServiceReturnedPokemon = ScenarioHelper_PokedexServiceContract.CreatePokemonBasic(pokemonName, "Pikachu's Description");

            cacheServiceMock.ExpectTryGetValue(GetPokemonNameCacheKey(pokemonName), null);

            pokemonInformationOrchestratorMock.ExpectGetPokemonDetailsAsync(pokemonName, fakeServiceReturnedPokemon);

            Models.PokemonBasic pokemonCacheValue = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "Pikachu's Description");

            cacheServiceMock.ExpectSet(GetPokemonNameCacheKey(pokemonName), pokemonCacheValue);

            ActionResult<Models.PokemonBasic> returnedPokemon = await pokemonController.Get(pokemonName);

            Assert.AreEqual(typeof(OkObjectResult), returnedPokemon.Result.GetType());

            Models.PokemonBasic expectedPokemon = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "Pikachu's Description");

            AssertPokemonProperties(expectedPokemon, (Models.PokemonBasic)(returnedPokemon.Result as OkObjectResult).Value);

            pokemonInformationOrchestratorMock.VerifyGetPokemonDetailsAsync(pokemonName);

            cacheServiceMock.VerifyTryGetValue(GetPokemonNameCacheKey(pokemonName));
            cacheServiceMock.VerifySet(GetPokemonNameCacheKey(pokemonName), pokemonCacheValue);
        }

        [TestMethod]
        public async Task Get_Pikachu_AlreadyCached_GetsPokemonFromCache_ReturnsExpectedPokemonWithoutCallingService()
        {
            string pokemonName = "Pikachu";

            Models.PokemonBasic pokemonCacheValue = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "Pikachu's Description");

            cacheServiceMock.ExpectTryGetValue(GetPokemonNameCacheKey(pokemonName), pokemonCacheValue);

            ActionResult<Models.PokemonBasic> returnedPokemon = await pokemonController.Get(pokemonName);

            Assert.AreEqual(typeof(OkObjectResult), returnedPokemon.Result.GetType());

            Models.PokemonBasic expectedPokemon = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "Pikachu's Description");

            AssertPokemonProperties(expectedPokemon, (Models.PokemonBasic)(returnedPokemon.Result as OkObjectResult).Value);

            pokemonInformationOrchestratorMock.VerifyGetPokemonDetailsAsync(pokemonName);

            cacheServiceMock.VerifyTryGetValue(GetPokemonNameCacheKey(pokemonName));
        }

        [TestMethod]
        public async Task Get_IncorrectPokemonName_ServiceReturnsNull_ControllerReturnsNotFound()
        {
            string pokemonName = "Pikachuuuuuuu";

            cacheServiceMock.ExpectTryGetValue(GetPokemonNameCacheKey(pokemonName), null);

            pokemonInformationOrchestratorMock.ExpectGetPokemonDetailsAsync(pokemonName, null);

            ActionResult<Models.PokemonBasic> returnedPokemon = await pokemonController.Get(pokemonName);

            Assert.AreEqual(typeof(NotFoundObjectResult), returnedPokemon.Result.GetType());

            Assert.AreEqual($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch", ((NotFoundObjectResult)returnedPokemon.Result).Value);

            pokemonInformationOrchestratorMock.VerifyGetPokemonDetailsAsync(pokemonName);
        }

        private void AssertPokemonProperties(Models.PokemonBasic expectedPokemonBasic, Models.PokemonBasic actualPokemonBasic)
        {
            Assert.IsNotNull(expectedPokemonBasic);
            Assert.IsNotNull(actualPokemonBasic);

            Assert.AreEqual(expectedPokemonBasic.Name, actualPokemonBasic.Name);
            Assert.AreEqual(expectedPokemonBasic.Description, actualPokemonBasic.Description);
            Assert.AreEqual(expectedPokemonBasic.Habitat, actualPokemonBasic.Habitat);
            Assert.AreEqual(expectedPokemonBasic.IsLegendary, actualPokemonBasic.IsLegendary);
        }
        private static string GetPokemonNameCacheKey(string pokemonName)
        {
            return $"{InternalDataContracts.CacheKeys.PokemonBasicCacheKeyPreffix }{pokemonName}";
        }
    }
}
