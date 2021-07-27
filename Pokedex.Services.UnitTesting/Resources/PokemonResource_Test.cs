using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.UnitTesting.Resources
{
    //This test class is used to test the mappings developper adds to data layer object (db column) 
    //  by testing at resource level with real implementation for dependencies, the DA classes are tested directly here
    //  On the long run this can pick up on missing script    
    [TestClass]
    public class PokemonResource_Test
    {
        IPokemonResource pokemonResource;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonResource = new PokemonResource(new PokemonDA(), UnitTestSetupHelper.CreateMapper());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Here - We should get rid of any resource during unit test since this would be testing any
            // framework integration to a DB provider or other IO dependencies created specifically for
            // the purpose of the tests.
        }

        [TestMethod]
        public void SelectByName_Pikachu_ReturnsPokemonWithNamedPassedAsParam_AndRestOfPropertiesHardcoded()
        {
            Services.Resources.Contract.PokemonBasic expectedPokemon = ScenarioHelper_ResourceContract.CreatePokemonBasic("Pikachu", "We don't yet have a description for Pikachu, but we promise, it's coming soon");

            Services.Resources.Contract.PokemonBasic result = pokemonResource.SelectByName("Pikachu");

            AssertPokemonProperties(expectedPokemon, result);
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
            Assert.AreEqual(expectedPokemonBasic.PokemonHabitat, actualPokemonBasic.PokemonHabitat);
            Assert.AreEqual(expectedPokemonBasic.IsLegendary, actualPokemonBasic.IsLegendary);
        }
    }
}
