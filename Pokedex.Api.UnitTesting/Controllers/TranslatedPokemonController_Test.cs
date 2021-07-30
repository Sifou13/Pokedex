using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Api.Controllers;
using Pokedex.Api.UnitTesting.UnitTestingHelpers;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting.Controllers
{
    [TestClass]
    public class TranslatedPokemonController_Test
    {
        private TranslatedPokemonController translatedPokemonController;
        private Mock<Services.Contract.Orchestrators.IPokemonInformationOrchestrator> pokemonInformationOrchestratorMock;

        [TestInitialize]
        public void Initialize()
        {
            pokemonInformationOrchestratorMock = new Mock<Services.Contract.Orchestrators.IPokemonInformationOrchestrator>(MockBehavior.Strict);

            translatedPokemonController = UnitTestSetupHelper.CreateTranslatedPokemonController(pokemonInformationOrchestratorMock);
        }

        [TestMethod]
        public async Task Index_DefaultApiEndpoint_ShouldNotBeHit_ProvideRelevantDocumentationToUserForCorrectUseOfApi()
        {
            string expectedTranslatedPokemonDefaultApiResponse = @"This API is designed around individual Pokemons. Choose a pokemon name to see a fun translation of their description. You can do this by adding it to the URL: currentURL + ""/mewtwo";
            
            ActionResult<string> result = await translatedPokemonController.Index();

            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());

            Assert.AreEqual(expectedTranslatedPokemonDefaultApiResponse, (result.Result as OkObjectResult).Value);
        }

        [TestMethod]
        public async Task Get_Pikachu_ReturnsOkStatusWithExpectedPokemonBasicDetails()
        {
            string pokemonName = "Pikachu";

            Pokedex.Services.Contract.PokemonBasic fakeServiceReturnedPokemon = ScenarioHelper_PokedexServiceContract.CreatePokemonBasic(pokemonName, "Pikachu's translated description");

            pokemonInformationOrchestratorMock.ExpectGetTranslatedPokemonDetailsAsync(pokemonName, fakeServiceReturnedPokemon);

            ActionResult<Models.PokemonBasic> returnedPokemon = await translatedPokemonController.Get(pokemonName);

            Assert.AreEqual(typeof(OkObjectResult), returnedPokemon.Result.GetType());

            Models.PokemonBasic expectedPokemon = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "Pikachu's translated description");

            AssertPokemonProperties(expectedPokemon, (Models.PokemonBasic)(returnedPokemon.Result as OkObjectResult).Value);

            pokemonInformationOrchestratorMock.VerifyGetPokemonDetailsAsync(pokemonName);
        }

        [TestMethod]
        public async Task Get_IncorrectPokemonName_ServiceReturnsNull_ControllerReturnsNotFound()
        {
            string pokemonName = "Pikachuuuuuuu";

            pokemonInformationOrchestratorMock.ExpectGetTranslatedPokemonDetailsAsync(pokemonName, null);

            ActionResult<Models.PokemonBasic> returnedPokemon = await translatedPokemonController.Get(pokemonName);

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
    }
}
