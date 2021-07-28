using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Translators;
using System;
namespace Pokedex.Services.Resources
{
    public class TranslationResource : ITranslationResource
    {
        private readonly IShakespeareTranslator shakespeareTranslator;
        private readonly IYodaTranslator yodaTranslator;

        public TranslationResource(IShakespeareTranslator shakespeareTranslator, IYodaTranslator yodaTranslator)
        {
            this.shakespeareTranslator = shakespeareTranslator;
            this.yodaTranslator = yodaTranslator;
        }
        
        public string Translate(string textToTranslate, Resources.Contract.PokemonHabitat habitat)
        {
            throw new NotImplementedException();
        }
    }
}
