using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.UnitTesting.Resources
{
    [TestClass]
    public class PokemonResource_Test
    {
        IPokemonResource pokemonResource;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonResource = new PokemonResource();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Here - We should get rid of any resource during unit test since this would be testing any
            // framework integration to a DB provider or other IO dependencies created specifically for
            // the purpose of the tests.
        }
    }
}
