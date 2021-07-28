using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon;
using Pokedex.Services.UnitTesting.UnitTestingHelpers;

namespace Pokedex.Services.UnitTesting.Resources
{
    //This test class is used to test the mappings developper adds to data layer object (db column) 
    //  by testing at resource level with real implementation for dependencies, the DA classes are tested directly here
    //  On the long run this can pick up on missing script    
    [TestClass]
    public class PokemonResource_Test
    {
        private IPokemonResource pokemonResource;
        private IConfig config;

        [TestInitialize]
        public void TestInitialize()
        {
            //Here - Config is using the currently Default Service Value
            //For Unit Testing, We should in theory:
            // 1 - Not be testing Third Pary Apis directly on CI gates
            // 2 - Create a Unit Testing Config so that Resource unit tests use test settings
            // such as connection strings that target test DBs 
            config = new Config();

            pokemonResource = new PokemonResource(new PokemonDA(config));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Here - We should get rid of any resource during unit test since this would be testing any
            // framework integration to a DB provider or other IO dependencies created specifically for
            // the purpose of the tests. - if this remain only calling external apis, it may become obsolete
            // but only here to show for the purpose of the demo
        }
        
        [TestMethod]
        public void SelectByName_mewtwo_ReturnsPokemonWithNamedPassedAsParam_AndRestOfPropertiesHardcoded()
        {
            Services.Resources.Contract.PokemonBasic expectedPokemon = 
                ScenarioHelper_ResourceContract
                .CreatePokemonBasic(
                    "mewtwo", 
                    "It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.",
                    pokemonHabitat: Services.Resources.Contract.PokemonHabitat.Rare,
                    IsLegendary: true);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("mewtwo");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_Magikarp_HasHabitatWithHyphenTesting_JsonConverters_HabitatCorrectlyParsed()
        {
            Services.Resources.Contract.PokemonBasic expectedPokemon =
                ScenarioHelper_ResourceContract
                .CreatePokemonBasic(
                    "magikarp",
                    "In the distant\npast, it was\nsomewhat stronger\fthan the horribly\nweak descendants\nthat exist today.",
                    pokemonHabitat: Services.Resources.Contract.PokemonHabitat.WatersEdge,
                    IsLegendary: false);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("magikarp");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_Rhydon_HasHabitatWithHyphen_JsonConverters_HabitatCorrectlyParsed()
        {
            Services.Resources.Contract.PokemonBasic expectedPokemon =
                ScenarioHelper_ResourceContract
                .CreatePokemonBasic(
                    "rhydon",
                    "Protected by an\narmor-like hide,\nit is capable of\fliving in molten\nlava of 3,600\ndegrees.",
                    pokemonHabitat: Services.Resources.Contract.PokemonHabitat.RoughTerrain,
                    IsLegendary: false);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("rhydon");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_ValidPokemon_NamePassedWithCapitalLetters_NameIsLoweredBeforeAPICallASNotSupportedByThirdParty()
        {
            Services.Resources.Contract.PokemonBasic expectedPokemon =
                ScenarioHelper_ResourceContract
                .CreatePokemonBasic(
                    "pikachu",
                    "When several of\nthese POKéMON\ngather, their\felectricity could\nbuild and cause\nlightning storms.",
                    pokemonHabitat: Services.Resources.Contract.PokemonHabitat.Forest,
                    IsLegendary: false);

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("PIKACHU");

            AssertPokemonProperties(expectedPokemon, result);
        }

        [TestMethod]
        public void SelectByName_InValidPokemon_ReturnsNull()
        {
            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("NotAPokemonName");

            Assert.IsNull(result);
        }

        //Since this is repetitive and will become with additional features and object:
        // we could refactor this helpers and integrate to a framework library (For Unit testing in this specific case)
        // using generics/reflection to test object differences and customize this to our technical/business needs        
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
