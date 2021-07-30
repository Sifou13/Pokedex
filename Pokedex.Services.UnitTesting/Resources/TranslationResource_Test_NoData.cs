using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokedex.Services.Resources;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;
using Pokedex.Services.UnitTesting.UnitTestingHelpers;

namespace Pokedex.Services.UnitTesting.Resources
{
    [TestClass]
    public class TranslationResource_Test_NoData
    {
        private Mock<ITranslator> translatorMock;

        private ITranslationResource translationResource;

        [TestInitialize]
        public void TestInitialize()
        {
            translatorMock = new Mock<ITranslator>(MockBehavior.Strict);
            translationResource = new TranslationResource(translatorMock.Object);
        }

        #region Shakespeare Translations

        [TestMethod]
        public void TranslateUsingShakespeare_TranslatorReturnsValidTranslation_ResourceReturnsTranslatedText()
        {
            translatorMock.ExpectTranslateUsingShakespeare(expectedTextToTranslate: "Hello", returnedTranslatedText: "Ola", 1);

            string result = translationResource.TranslateUsingShakespeare("Hello");

            Assert.AreEqual("Ola", result);

            translatorMock.VerifyTranslateUsingShakespeare("Hello");
        }

        [TestMethod]
        public void TranslateUsingShakespeare_TranslatorReturnsNullResult_ResourceReturnsOriginalText()
        {
            translatorMock.ExpectTranslateUsingShakespeare(expectedTextToTranslate: "Hello", returnedTranslatedText: null);

            string result = translationResource.TranslateUsingShakespeare("Hello");

            Assert.AreEqual("Hello", result);

            translatorMock.VerifyTranslateUsingShakespeare("Hello");
        }

        [TestMethod]
        public void TranslateUsingShakespeare_TranslatorReturnsSuccessFlagWithZero_ResourceReturnsOriginalText()
        {
            translatorMock.ExpectTranslateUsingShakespeare(expectedTextToTranslate: "Hello", returnedTranslatedText: "asjhaslkhaslkh", 0);

            string result = translationResource.TranslateUsingShakespeare("Hello");

            Assert.AreEqual("Hello", result);

            translatorMock.VerifyTranslateUsingShakespeare("Hello");
        }

        [TestMethod]
        public void TranslateUsingShakespeare_TextToTranslateIsNull_ResourceReturnsNullWithoutCallingUnderlyingAPI_SavesCall()
        {
            string result = translationResource.TranslateUsingShakespeare(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TranslateUsingShakespeare_TextToTranslateIsEmptyString_ResourceReturnsNullWithoutCallingUnderlyingAPI_SavesCall()
        {
            string result = translationResource.TranslateUsingShakespeare(string.Empty);

            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region Yoda Translations

        [TestMethod]
        public void TranslateUsingYoda_TranslatorReturnsValidTranslation_ResourceReturnsTranslatedText()
        {
            translatorMock.ExpectTranslateUsingYoda(expectedTextToTranslate: "Hello", returnedTranslatedText: "Ola", 1);

            string result = translationResource.TranslateUsingYoda("Hello");

            Assert.AreEqual("Ola", result);

            translatorMock.VerifyTranslateUsingYoda("Hello");
        }

        [TestMethod]
        public void TranslateUsingYoda_TranslatorReturnsNullResult_ResourceReturnsOriginalText()
        {
            translatorMock.ExpectTranslateUsingYoda(expectedTextToTranslate: "Hello", returnedTranslatedText: null);

            string result = translationResource.TranslateUsingYoda("Hello");

            Assert.AreEqual("Hello", result);

            translatorMock.VerifyTranslateUsingYoda("Hello");
        }

        [TestMethod]
        public void TranslateUsingYoda_TranslatorReturnsSuccessFlagWithZero_ResourceReturnsOriginalText()
        {
            translatorMock.ExpectTranslateUsingYoda(expectedTextToTranslate: "Hello", returnedTranslatedText: "asjhaslkhaslkh", 0);

            string result = translationResource.TranslateUsingYoda("Hello");

            Assert.AreEqual("Hello", result);

            translatorMock.VerifyTranslateUsingYoda("Hello");
        }

        [TestMethod]
        public void TranslateUsingYoda_TextToTranslateIsNull_ResourceReturnsNullWithoutCallingUnderlyingAPI_SavesCall()
        {
            string result = translationResource.TranslateUsingYoda(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TranslateUsingYoda_TextToTranslateIsEmptyString_ResourceReturnsNullWithoutCallingUnderlyingAPI_SavesCall()
        {
            string result = translationResource.TranslateUsingYoda(string.Empty);

            Assert.AreEqual(string.Empty, result);
        }

        #endregion
    }
}
