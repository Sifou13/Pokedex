using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Pokedex.Services.UnitTesting
{
    [TestClass]
    public class ContractsMatchingTypes_Test
    {
        //Those test would force a software engineer adding a new Habitat as part of a feature request, and forgetting to add it on one of the service (logical)
        //layers
        [TestMethod]
        public void Assert_PokemonHabitatEnum_PublicContract_ResourceContract_AreEqual()
        {
            Assert.IsTrue(((Contract.PokemonHabitat[])Enum.GetValues(typeof(Contract.PokemonHabitat))).SequenceEqual((Contract.PokemonHabitat[])Enum.GetValues(typeof(Services.Resources.Contract.PokemonHabitat))));
        }

        [TestMethod]
        public void Assert_PokemonHabitatEnum_PublicContract_ResourceDataAccess_AreEqual()
        {
            Assert.IsTrue(((Contract.PokemonHabitat[])Enum.GetValues(typeof(Contract.PokemonHabitat))).SequenceEqual((Contract.PokemonHabitat[])Enum.GetValues(typeof(Services.Resources.DataAccess.Pokemon.PokemonHabitat))));
        }
    }
}
