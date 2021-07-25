using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Resources.Contract
{
    public interface IPokemonTranslationResource
    {
        string Translate(string textToTranslate, Resources.Contract.PokemonHabitat habitat);
    }
}
