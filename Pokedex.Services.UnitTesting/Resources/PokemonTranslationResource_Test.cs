using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;

//This is added a skeleton to show that something need to be done here:
// due to rate limit we can not test directly, so like in other places,
// we need to mock the web requests but also have some real test in CI pipelines (depending on strategy/frequency of deployment)
namespace Pokedex.Services.UnitTesting.Resources
{
    [TestClass]
    public class PokemonTranslationResource_Test
    {
        private ITranslationResource pokemonTranslationResource;
        private ITranslator translator;

        [TestInitialize]
        public void TestInitialize()
        {
            pokemonTranslationResource = new TranslationResource(translator);
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
