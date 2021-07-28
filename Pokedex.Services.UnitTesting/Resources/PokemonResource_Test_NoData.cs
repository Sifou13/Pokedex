using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon;
using Pokedex.Services.UnitTesting.UnitTestingHelpers;

namespace Pokedex.Services.UnitTesting.Resources
{
    //Showing how we can have two types of unit test (Data /No Data) and find and agree the balance between the two (Criticality / Team standards/agreements) can ensure
    // quality while at the same time preventing team blockers if they are caught during CI pipelines (as CI build increase more with data tests
    //than with mocked ones)
    [TestClass]
    public class PokemonResource_Test_NoData
    {
        private Mock<IPokemonDA> pokemonDAMock;
        
        private IPokemonResource pokemonResource;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonDAMock = new Mock<IPokemonDA>(MockBehavior.Strict);
            pokemonResource = new PokemonResource(pokemonDAMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Here - We should get rid of any resource during unit test since this would be testing any
            // framework integration to a DB provider or other IO dependencies created specifically for
            // the purpose of the tests.
        }

        [TestMethod]
        public void SelectByName_ValidScenario_PokemonApiAggregation_Success()
        {
            string returnedSpeciesUrl = "Species URL";

            pokemonDAMock.ExpectSelectByName("pikachu", new PokemonRoot { Name = "pikachu", Species = new PokemonSpeciesLink { Name = "pikachu", Url = returnedSpeciesUrl } });

            PokemonSpecies fakelyReturnedSpecies = new PokemonSpecies { Habitat = new Habitat { Name = Services.Resources.DataAccess.Pokemon.PokemonHabitat.Forest }, Is_Legendary = true };

            fakelyReturnedSpecies
                .AddFlavorTextEntry("me llamo", "es")
                .AddFlavorTextEntry("mi chiamo", "it")
                .AddFlavorTextEntry("my name is", "en")
                .AddFlavorTextEntry("je m'appelle", "fr")
                .AddFlavorTextEntry("I can be called", "en");

            pokemonDAMock.ExpectSelectSpeciesByUrl(returnedSpeciesUrl, fakelyReturnedSpecies);

            Services.Resources.Contract.PokemonBasic expectedPokemon =
                ScenarioHelper_ResourceContract
                .CreatePokemonBasic(
                    "pikachu",
                    "my name is",
                    pokemonHabitat: Services.Resources.Contract.PokemonHabitat.Forest,
                    IsLegendary: true);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("pikachu");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_PokemonApiAggregation_MissingSpeciesDetails_ReturnPokemonMinimalInfo()
        {
            pokemonDAMock.ExpectSelectByName("pikachu", new PokemonRoot { Name = "pikachu" });

            Services.Resources.Contract.PokemonBasic expectedPokemon = 
                ScenarioHelper_ResourceContract 
                    .CreatePokemonBasic(
                        "pikachu", 
                        "Could not be loaded...", 
                        Services.Resources.Contract.PokemonHabitat.None,
                        false);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("pikachu");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_PokemonApiAggregation_MissingSpeciesUrl_ReturnPokemonMinimalInfo()
        {
            pokemonDAMock.ExpectSelectByName("pikachu", new PokemonRoot { Name = "pikachu", Species = new PokemonSpeciesLink { Name = "pikachu" } });

            Services.Resources.Contract.PokemonBasic expectedPokemon = 
                ScenarioHelper_ResourceContract
                    .CreatePokemonBasic(
                        "pikachu", 
                        "Could not be loaded...",
                        Services.Resources.Contract.PokemonHabitat.None,
                        false);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("pikachu");

            AssertPokemonProperties(expectedPokemon, result);
        }

        private void AssertPokemonProperties(Services.Resources.Contract.PokemonBasic expectedPokemonBasic, Services.Resources.Contract.PokemonBasic actualPokemonBasic)
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
