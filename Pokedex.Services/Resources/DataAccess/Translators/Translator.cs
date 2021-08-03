using Pokedex.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services.Resources.DataAccess.Translators
{
    public interface ITranslator
    {
        Task<Contract.TranslationResult> TranslateUsingShakespeareAsync(string textToTranslate);

        Task<Contract.TranslationResult> TranslateUsingYodaAsync(string textToTranslate);
    }

    //This translator sits below the resource calling it
    //it encapsulates detail around this specific API providers - it can be changed without triggering changes outside
    //The resource remain free to add new end points and new/other data end point
    //The granularity of the methods also allow us to use a different provider for yoda by moving it from here without affecting the code
    // used for Yoda
    //I did not add test for this for the purpose of the demo, but it should be unit tested (Mocking the web request)
    public class Translator : ITranslator
    {
        private readonly string TranslationInputQueryStringName = "text";
        private readonly string ShakespeareApiTranslationTypeIdentifier = "shakespeare";
        private readonly string YodaApiTranslationTypeIdentifier = "yoda";

        private readonly IConfig config;

        public Translator(IConfig config)
        {
            this.config = config;
        }

        public async Task<Contract.TranslationResult> TranslateUsingShakespeareAsync(string textToTranslate)
        {
            return await Translate(textToTranslate, ShakespeareApiTranslationTypeIdentifier);
        }

        public async Task<Contract.TranslationResult> TranslateUsingYodaAsync(string textToTranslate)
        {
            return await Translate(textToTranslate, YodaApiTranslationTypeIdentifier);
        }

        private async Task<Contract.TranslationResult> Translate(string textToTranslate, string apiTranslationTypeIdentifier)
        {
            Dictionary<string, string> queryStrings = new Dictionary<string, string>();
            queryStrings.Add(TranslationInputQueryStringName, textToTranslate);

            string requestedUrl = $@"{config.FunTranslationApiUrl}/{apiTranslationTypeIdentifier}.json";

            try
            {
                return await WebRequestHelper.Get<Contract.TranslationResult>(requestedUrl, queryStrings, CreateJsonOptions());
            }
            catch 
            {
                return null;
            }
        }
        
        private JsonSerializerOptions CreateJsonOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
    }
}
