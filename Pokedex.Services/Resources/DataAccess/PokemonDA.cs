using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Resources.DataAccess
{
    public interface IPokemonDA
    {
        PokemonBasic DetailByName(string name);
    }

    public class PokemonDA : IPokemonDA
    {
        public PokemonBasic DetailByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
