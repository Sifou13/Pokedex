using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;

namespace Pokedex.Services.UnitTesting.Resources
{
    [TestClass]
    public class PokemonTranslationResource_Test
    {
        private ITranslationResource pokemonTranslationResource;
        private IShakespeareTranslator shakespeareTranslator;
        private IYodaTranslator yodaTranslator;

        [TestInitialize]
        public void TestInitialize()
        {
            shakespeareTranslator = new ShakespeareTranslator();
            yodaTranslator = new YodaTranslator();

            pokemonTranslationResource = new TranslationResource(shakespeareTranslator, yodaTranslator);
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
