using System;

namespace Pokedex.Services.Resources.DataAccess.Translators
{
    public interface IYodaTranslator
    {
        string Translate(string textToTranslate);
    }

    public class YodaTranslator : IYodaTranslator
    {
        public string Translate(string textToTranslate)
        {
            throw new NotImplementedException();
        }
    }
}
