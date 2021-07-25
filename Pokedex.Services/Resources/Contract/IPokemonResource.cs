using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Resources.Contract
{
    public interface IPokemonResource
    {
        PokemonBasic Select(string name);
    }
}
