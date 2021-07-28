using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services.Resources.DataAccess.Pokemon
{
    public interface INamedAPIResource
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
