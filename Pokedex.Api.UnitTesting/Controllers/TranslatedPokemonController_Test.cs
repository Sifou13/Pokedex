using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Api.Controllers;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting.Controllers
{
    [TestClass]
    public class TranslatedPokemonController_Test
    {
        private TranslatedPokemonController translatedPokemonController;

        [TestInitialize]
        public void Initialize()
        {
            translatedPokemonController = new TranslatedPokemonController();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Reminder - To verify all your mocks and clear any other context dependencies as soon as you make use of them in UT
        }

        [TestMethod]
        public async Task Index_DefaultApiEndpoint_ShouldNotBeHit_ProvideRelevantDocumentationToUserForCorrectUseOfApi()
        {
            string expectedTranslatedPokemonDefaultApiResponse = @"This API is designed around individual Pokemons./n/nChoose a pokemon name to see a fun translation of their description./rYou can do this by adding it to the URL: currentURL + ""/mewtwo";
            
            ActionResult<string> result = await translatedPokemonController.Index();

            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());

            Assert.AreEqual(expectedTranslatedPokemonDefaultApiResponse, (result.Result as OkObjectResult).Value);
        }

        [TestMethod]
        public async Task Get_RequestForPokemonName_ReturnsOkStatusWithStringStatingTheApiIsNotReadyYet()
        {
            string expectedTemporaryTranslatedPokemonResponse = $"Sorry, Mewtwo's information have not yet been made available.....so, we can't yet translate their desription, bare with us.";

            ActionResult<string> result = await translatedPokemonController.Get("Mewtwo");

            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());

            Assert.AreEqual(expectedTemporaryTranslatedPokemonResponse, (result.Result as OkObjectResult).Value);
        }

    }
}
