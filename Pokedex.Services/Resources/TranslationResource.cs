using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;
using System;
using System.Threading.Tasks;

namespace Pokedex.Services.Resources
{
    //This resource has the responsibility to carry translation by calling the relevant Data Access point
    //it helps gather all the translation resources in the same place and encapsulate them from higher up
    //It also drives win a consistent way how we want certain resources to behave in certain conditions (Null, API not available, etc..)
    public class TranslationResource : ITranslationResource
    {
        private readonly ITranslator translator;
        
        //This service class will allow to aggregate or call DA wrappers to different third parties that could be used for other translations
        public TranslationResource(ITranslator translator)
        {
            this.translator = translator;
        }

        public string TranslateUsingShakespeare(string textToTranslate)
        {
            return Translate(textToTranslate, async () => { return await translator.TranslateUsingShakespeareAsync(textToTranslate); });
        }

        public string TranslateUsingYoda(string textToTranslate)
        {
            return Translate(textToTranslate, async () => { return await translator.TranslateUsingYodaAsync(textToTranslate); });
        }

        private string Translate(string textToTranslate, Func<Task<DataAccess.Translators.Contract.TranslationResult>> provideTranslateOperationAsync)
        {
            if (string.IsNullOrEmpty(textToTranslate))
                return textToTranslate;

            DataAccess.Translators.Contract.TranslationResult translationResult = provideTranslateOperationAsync().Result;

            if (translationResult == null || translationResult.Success.Total == 0)
                return textToTranslate;

            return translationResult.Contents.Translated;
        }
    }
}
