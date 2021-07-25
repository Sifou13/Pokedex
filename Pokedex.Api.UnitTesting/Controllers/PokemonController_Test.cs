using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Api.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting.Controllers
{
    [TestClass]
    public class PokemonController_Test
    {
        private PokemonController pokemonController;

        [TestInitialize]
        public void Initialize()
        {
            pokemonController = new PokemonController();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Here Being Pragmatic again and going against YAGNI since not needed now
            //However this is a base / initial framework that other engineers may start contributing to
            //so it may be good as a reminder that any mock or other dependencie will have to be disposed of here
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
            string expectedPokemonResponseTemporaryMessage = $"Sorry, Pikachu's information have not yet been made available.....";

            ActionResult<string> result = await pokemonController.Get("Pikachu");

            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());
            
            Assert.AreEqual(expectedPokemonResponseTemporaryMessage, (result.Result as OkObjectResult).Value);
        }

    }
}
