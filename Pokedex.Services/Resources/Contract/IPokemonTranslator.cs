using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Resources.Contract
{
    public interface ITranslationResource
    {
        string TranslateUsingShakespeare(string textToTranslate);
        
        string TranslateUsingYoda(string textToTranslate);
    }
}
