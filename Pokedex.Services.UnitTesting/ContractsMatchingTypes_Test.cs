using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Pokedex.Services.UnitTesting
{
    [TestClass]
    public class ContractsMatchingTypes_Test
    {
        [TestMethod]
        public void Assert_PokemonHabitatEnum_PublicContract_ResourceContract_AreEqual()
        {
            Assert.IsTrue(((Contract.PokemonHabitat[])Enum.GetValues(typeof(Contract.PokemonHabitat))).SequenceEqual((Contract.PokemonHabitat[])Enum.GetValues(typeof(Services.Resources.Contract.PokemonHabitat))));
        }

        [TestMethod]
        public void Assert_PokemonHabitatEnum_PublicContract_ResourceDataAccess_AreEqual()
        {
            Assert.IsTrue(((Contract.PokemonHabitat[])Enum.GetValues(typeof(Contract.PokemonHabitat))).SequenceEqual((Contract.PokemonHabitat[])Enum.GetValues(typeof(Services.Resources.DataAccess.PokemonHabitat))));
        }
    }
}
