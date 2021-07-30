using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Services.Contract.Orchestrators;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.UnitTesting.UnitTestingHelpers;
using System.Threading.Tasks;

namespace Pokedex.Services.UnitTesting.Orchestrators
{
    [TestClass]
    public class PokemonInformationOrchestrator_Test_GetTranslatedPokemonDetailsAsync
    {
        private Mock<IPokemonResource> pokemonResource;
        private Mock<ITranslationResource> translationResource;

        private IPokemonInformationOrchestrator pokemonInformationOrchestrator;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonResource = new Mock<IPokemonResource>(MockBehavior.Strict);
            translationResource = new Mock<ITranslationResource>(MockBehavior.Strict);

            pokemonInformationOrchestrator = UnitTestSetupHelper.CreatePokemonInformationOrchestratorService(pokemonResource, translationResource);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            pokemonResource.VerifyAll();
        }
                
        [TestMethod]
        public async Task GetTranslatedPokemonDetailsAsync_HasCaveHabitat_ExpectsYodaTranslation()
        {
            string pokemonName = "pikachu";

            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = GetFakelyRetrievedPokemon(pokemonName, "description to translate", Services.Resources.Contract.PokemonHabitat.Cave);

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);

            translationResource.ExpectTranslateUsingYoda("description to translate", "Translated description");

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

            Contract.PokemonBasic expectedPokemonBasic = GetExpectedPokemonBasic(pokemonName, "Translated description", PokemonHabitat.Cave); 
            
            AssertPokemonProperties(expectedPokemonBasic, actual);

            pokemonResource.VerifySelectByName(pokemonName);
            translationResource.VerifyTranslateUsingYoda("description to translate");
        }

        [TestMethod]
        public async Task GetTranslatedPokemonDetailsAsync_IsLendaryStatusTrue_ExpectsYodaTranslation()
        {
            string pokemonName = "pikachu";

            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = GetFakelyRetrievedPokemon(pokemonName, "description to translate", isLegendary: true);

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);

            translationResource.ExpectTranslateUsingYoda("description to translate", "Translated description");

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

            Contract.PokemonBasic expectedPokemonBasic = GetExpectedPokemonBasic(pokemonName, "Translated description", isLegendary: true);

            AssertPokemonProperties(expectedPokemonBasic, actual);

            pokemonResource.VerifySelectByName(pokemonName);
            translationResource.VerifyTranslateUsingYoda("description to translate");
        }

        [TestMethod]
        public async Task GetTranslatedPokemonDetailsAsync_HabitatOtherThanCave_LegendaryStatusFalse_ExpectsShakespeareTranslation()
        {
            string pokemonName = "pikachu";

            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = GetFakelyRetrievedPokemon(pokemonName, "description to translate", PokemonHabitat.Forest, isLegendary: false);

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);

            translationResource.ExpectTranslateUsingShakespeare("description to translate", "Translated description");

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

            Contract.PokemonBasic expectedPokemonBasic = GetExpectedPokemonBasic(pokemonName, "Translated description", PokemonHabitat.Forest, isLegendary: false);

            AssertPokemonProperties(expectedPokemonBasic, actual);

            pokemonResource.VerifySelectByName(pokemonName);
            translationResource.VerifyTranslateUsingShakespeare("description to translate");
        }

        [TestMethod]
        public async Task GetTranslatedPokemonDetailsAsync_TranslatorResourceCouldNotTranslateDescription_ExpectsOriginalPokemonDescription()
        {
            string pokemonName = "pikachu";

            Services.Resources.Contract.PokemonBasic fakelyRetrievedPokemon = GetFakelyRetrievedPokemon(pokemonName, "description to translate", PokemonHabitat.Forest, isLegendary: false);

            pokemonResource.ExpectSelectByName(pokemonName, fakelyRetrievedPokemon);

            translationResource.ExpectTranslateUsingShakespeare("description to translate", null);

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

            Contract.PokemonBasic expectedPokemonBasic = GetExpectedPokemonBasic(pokemonName, "description to translate", PokemonHabitat.Forest, isLegendary: false);

            AssertPokemonProperties(expectedPokemonBasic, actual);

            pokemonResource.VerifySelectByName(pokemonName);
            translationResource.VerifyTranslateUsingShakespeare("description to translate");
        }

        [TestMethod]
        public async Task GetTranslatedPokemonDetailsAsync_PokemonWasNotRetrieved_OperationReturnsNull_NotTranslationOperationExpected()
        {
            string pokemonName = "pikachu";

            pokemonResource.ExpectSelectByName(pokemonName, null);

            Contract.PokemonBasic actual = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

            Assert.IsNull(actual);
        }

        private void AssertPokemonProperties(Contract.PokemonBasic expectedPokemonBasic, Contract.PokemonBasic actualPokemonBasic)
        {
            Assert.IsNotNull(expectedPokemonBasic);
            Assert.IsNotNull(actualPokemonBasic);

            Assert.AreEqual(expectedPokemonBasic.Name, actualPokemonBasic.Name);
            Assert.AreEqual(expectedPokemonBasic.Description, actualPokemonBasic.Description);
            Assert.AreEqual(expectedPokemonBasic.Habitat, actualPokemonBasic.Habitat);
            Assert.AreEqual(expectedPokemonBasic.IsLegendary, actualPokemonBasic.IsLegendary);
        }

        private static Contract.PokemonBasic GetExpectedPokemonBasic(string pokemonName, string description, PokemonHabitat pokemonHabitat = PokemonHabitat.Sea, bool isLegendary = false)
        {
            return ScenarioHelper_Contract.CreatePokemonBasic(
                pokemonName, 
                description, 
                (Contract.PokemonHabitat)pokemonHabitat,
                isLegendary);
        }

        private static PokemonBasic GetFakelyRetrievedPokemon(string pokemonName, string description, PokemonHabitat pokemonHabitat = PokemonHabitat.Sea, bool isLegendary = false)
        {
            return ScenarioHelper_ResourceContract.CreatePokemonBasic(
                pokemonName,
                description,
                pokemonHabitat,
                isLegendary);
        }

    }
}
