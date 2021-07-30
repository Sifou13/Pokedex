using Moq;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;
using Pokedex.Services.Resources.DataAccess.Translators.Contract;
using System.Threading.Tasks;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class MockHelper
    {
        #region Pokemon Resource
        public static void ExpectSelectByName(this Mock<IPokemonResource> pokemonResource, string name, Pokedex.Services.Resources.Contract.PokemonBasic pokemonToReturn)
        {
            pokemonResource
                .Setup(x => x.SelectByName(name))
                .Returns(pokemonToReturn)
                .Verifiable();
        }

        public static void VerifySelectByName(this Mock<IPokemonResource> pokemonResource, string name, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectByName(name), callCount ?? Times.AtMostOnce());
        }
        #endregion

        #region PokemonDA
        public static void ExpectSelectByName(this Mock<IPokemonDA> pokemonDA, string name, PokemonRoot pokemonRootToReturn)
        {
            pokemonDA
                .Setup(x => x.SelectByName(name))
                .Returns(Task.FromResult(pokemonRootToReturn))
                .Verifiable();
        }

        public static void VerifySelectByName(this Mock<IPokemonDA> pokemonResource, string name, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectByName(name), callCount ?? Times.AtMostOnce());
        }

        public static void ExpectSelectSpeciesByUrl(this Mock<IPokemonDA> pokemonDA, string speciesURL, PokemonSpecies pokemonSpeciesToReturn)
        {
            pokemonDA
                .Setup(x => x.SelectSpeciesByUrl(speciesURL))
                .Returns(Task.FromResult(pokemonSpeciesToReturn))
                .Verifiable();
        }

        public static void VerifySelectSpeciesByUrl(this Mock<IPokemonDA> pokemonResource, string speciesURL, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectSpeciesByUrl(speciesURL), callCount ?? Times.AtMostOnce());
        }
        #endregion


        #region TranslationResource
        public static void ExpectTranslateUsingShakespeare(this Mock<ITranslationResource> translationResource, string expectedTextToTranslate, string returnedTranslatedText)
        {
            translationResource
                .Setup(x => x.TranslateUsingShakespeare(expectedTextToTranslate))
                .Returns(returnedTranslatedText)
                .Verifiable();
        }

        public static void VerifyTranslateUsingShakespeare(this Mock<ITranslationResource> pokemonResource, string translationResource, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.TranslateUsingShakespeare(translationResource), callCount ?? Times.AtMostOnce());
        }

        public static void ExpectTranslateUsingYoda(this Mock<ITranslationResource> translationResource, string expectedTextToTranslate, string returnedTranslatedText)
        {
            translationResource
                .Setup(x => x.TranslateUsingYoda(expectedTextToTranslate))
                .Returns(returnedTranslatedText)
                .Verifiable();
        }

        public static void VerifyTranslateUsingYoda(this Mock<ITranslationResource> pokemonResource, string translationResource, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.TranslateUsingShakespeare(translationResource), callCount ?? Times.AtMostOnce());
        }

        #endregion

        #region Translator
        public static void ExpectTranslateUsingShakespeare(this Mock<ITranslator> translator, string expectedTextToTranslate, string returnedTranslatedText, int? translationSuccessCount = null)
        {
            translator
                .Setup(x => x.TranslateUsingShakespeareAsync(expectedTextToTranslate))
                .Returns(CreateReturnedTranslationResult(returnedTranslatedText, translationSuccessCount))
                .Verifiable();
        }

        public static void ExpectTranslateUsingYoda(this Mock<ITranslator> translator, string expectedTextToTranslate, string returnedTranslatedText, int? translationSuccessCount = null)
        {
            translator
                .Setup(x => x.TranslateUsingYodaAsync(expectedTextToTranslate))
                .Returns(CreateReturnedTranslationResult(returnedTranslatedText, translationSuccessCount))
                .Verifiable();
        }

        private static Task<TranslationResult> CreateReturnedTranslationResult(string returnedTranslatedText, int? translationSuccessCount = null)
        {
            if (returnedTranslatedText == null && translationSuccessCount == null)
                return Task.FromResult((TranslationResult)null);

            return Task.FromResult(new Services.Resources.DataAccess.Translators.Contract.TranslationResult
            {
                Success = new Services.Resources.DataAccess.Translators.Contract.Success { Total = translationSuccessCount.Value },
                Contents = new Services.Resources.DataAccess.Translators.Contract.Contents { Translated = returnedTranslatedText }
            });
        }

        public static void VerifyTranslateUsingShakespeare(this Mock<ITranslator> translator, string translationResource, Times? callCount = null)
        {
            translator.Verify(x => x.TranslateUsingShakespeareAsync(translationResource), callCount ?? Times.AtMostOnce());
        }

        public static void VerifyTranslateUsingYoda(this Mock<ITranslator> translator, string translationResource, Times? callCount = null)
        {
            translator.Verify(x => x.TranslateUsingShakespeareAsync(translationResource), callCount ?? Times.AtMostOnce());
        }
        #endregion
    }
}
