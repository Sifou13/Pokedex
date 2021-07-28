using System;

namespace Pokedex.Services.Resources.DataAccess.Translators
{
    public interface IShakespeareTranslator
    {
        string Translate(string textToTranslate);
    }

    public class ShakespeareTranslator : IShakespeareTranslator
    {
        public string Translate(string textToTranslate)
        {
            throw new NotImplementedException();
        }
    }
}
