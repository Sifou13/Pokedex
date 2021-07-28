using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Services.Contract;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.UnitTesting.UnitTestingHelpers;
using System.Threading.Tasks;

namespace Pokedex.Services.UnitTesting.Orchestrators
{
    [TestClass]
    public class PokemonInformationOrchestrator_Test_GetPokemonDetails
    {
        //Here - the boundary for mocking has been set at the Resource level and we fake the data returned to test the orchestration as a unit
        //In some cases, and at team level decision, we may sometimes decide to go one level lower
        //if we wanted to only isolate external dependancies, and use moq expectations to assert on the resource 
        // expectations for each specific use case
        
        private Mock<IPokemonResource> pokemonResource;

        private IPokemonInformationOrchestrator pokemonInformationOrchestrator;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonResource = new Mock<IPokemonResource>(MockBehavior.Strict);

            pokemonInformationOrchestrator = UnitTestSetupHelper.CreatePokemonInformationOrchestratorService(pokemonResource);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            pokemonResource.VerifyAll();
        }
                
        // As usual, we try to remove any plumbing or code coming in the way to keep the unit test readable and meaningful to the reader
        // Taking advantage of the latest versions of MsTest to keep the testing Async, which should show performance improvement (when compared to normal)
        // if running tests in parallel
        [TestMethod]
        public async Task GetPokemonDetailsAsync_ValidPokemonName_ReturnsPokemonBasicWithAllExpectedInfo()
        {
            string pokemonName = "johny";

            //Adding helpers to create objects and balancing between static helpers and extension on objects to create nested members
            //This helps avoiding large taking most of the screen while seeing the properties being passed (or default those that are for most use cases with optional params)
            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = ScenarioHelper_ResourceContract.CreatePokemonBasic(pokemonName, "I am a string, different from my actual implementation - Good enough for the purpose of the test and avoid the overhead of IO while testing my whole use case");

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);
            
            Contract.PokemonBasic expectedPokemonBasic = ScenarioHelper_Contract.CreatePokemonBasic(pokemonName, "I am a string, different from my actual implementation - Good enough for the purpose of the test and avoid the overhead of IO while testing my whole use case");

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonName);

            AssertPokemonProperties(expectedPokemonBasic, actual);

            //By moving and wrapping all the mocks in one or a few helpers, we isolate the moq dependency so that we can keep up to date 
            // with the latest stable version
            pokemonResource.VerifySelectByName(pokemonName);
        }

        [TestMethod]
        public async Task GetPokemonDetailsAsync_PokemonName_HasUpperCases_ReturnsPokemonBasicWithAllExpectedInfo()
        {
            string pokemonName = "PIKACHU";
            string expectedPokemonUrlParam = pokemonName.ToLower();

            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = ScenarioHelper_ResourceContract.CreatePokemonBasic(expectedPokemonUrlParam, "I am a string, different from my actual implementation - Good enough for the purpose of the test and avoid the overhead of IO while testing my whole use case");

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);

            Contract.PokemonBasic expectedPokemonBasic = ScenarioHelper_Contract.CreatePokemonBasic(expectedPokemonUrlParam, "I am a string, different from my actual implementation - Good enough for the purpose of the test and avoid the overhead of IO while testing my whole use case");

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonName);

            AssertPokemonProperties(expectedPokemonBasic, actual);

            pokemonResource.VerifySelectByName(pokemonName);
        }

        //This will have to go to a unit testing framework library where it can be converted to a helper that 
        //compare two objects, and compare strictly (based on Type, not object) or not the objects members
        private void AssertPokemonProperties(Contract.PokemonBasic expectedPokemonBasic, Contract.PokemonBasic actualPokemonBasic)
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
