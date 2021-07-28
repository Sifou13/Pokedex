using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Api.Controllers;
using Pokedex.Services.Contract;
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
        
        [TestInitialize]
        public void Initialize()
        {
            pokemonInformationOrchestratorMock = new Mock<IPokemonInformationOrchestrator>(MockBehavior.Strict);

            pokemonController = UnitTestSetupHelper.CreatePokemonController(pokemonInformationOrchestratorMock);
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
        public async Task Get_Pikachu_ReturnsOkStatusWithStringStatingTheApiIsNotReadyYet()
        {
            string pokemonName = "Pikachu";

            Pokedex.Services.Contract.PokemonBasic fakeServiceReturnedPokemon = ScenarioHelper_PokedexServiceContract.CreatePokemonBasic(pokemonName, "We don't yet have a description for Pikachu, but we promise, it's coming soon");

            pokemonInformationOrchestratorMock.ExpectGetPokemonDetailsAsync(pokemonName, fakeServiceReturnedPokemon);
            
            ActionResult<string> returnedPokemon = await pokemonController.Get(pokemonName);

            Assert.AreEqual(typeof(OkObjectResult), returnedPokemon.Result.GetType());

            Models.PokemonBasic expectedPokemon = ScenarioHelper_ApiModels.CreatePokemon(pokemonName, "We don't yet have a description for Pikachu, but we promise, it's coming soon");

            AssertPokemonProperties(expectedPokemon, (Models.PokemonBasic)(returnedPokemon.Result as OkObjectResult).Value);

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
