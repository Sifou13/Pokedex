using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Services.Contract;
using Pokedex.Services.Orchestrators;
using Pokedex.Services.Resources.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.UnitTesting.Orchestrators
{
    [TestClass]
    public class PokemonInformationOrchestrator_Test_GetPokemonDetails
    {        
        private Mock<IPokemonResource> pokemonResource;

        private IPokemonInformationOrchestrator pokemonInformationOrchestrator;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonResource = new Mock<IPokemonResource>(MockBehavior.Strict);

            pokemonInformationOrchestrator = new PokemonInformationOrchestrator(pokemonResource.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Here - because we're mocking IO access (DB) we should not need to get rid of any resources

            // However, we should be verifying that all mock expectations were met - This enforce keeping code clean in UT projects and not leaving unused mock expectations
        }
    }
}
