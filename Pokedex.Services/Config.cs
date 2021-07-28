using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services
{
    public interface IConfig
    {
        public string PokemonAPIUrl { get; }
        
        public string ShakespeareTranslatorUrl { get; }
        
        public string YodaTranslatorUrl { get; }
    }

    public class Config : IConfig
    {
        public Config()
        {
            //These are readonly and should be config settings that differ from environemnt to environment
            // if we owned those Apis below, we would probably not use the production ones for testing involving CRUD ops (writting ops)
            //Here - We could be loading App Level Data via Json (used to be xml) at startup when this moves to its own "physical" service
            // or use the best practice for the service host chosen
            PokemonAPIUrl = "https://pokeapi.co/api/v2/";
            ShakespeareTranslatorUrl = "https://funtranslations.com/api/shakespeare";
            YodaTranslatorUrl = "https://funtranslations.com/api/yoda";
        }
        
        public string PokemonAPIUrl { get; }
         
        public string ShakespeareTranslatorUrl { get ; }
        
        public string YodaTranslatorUrl { get ; }
    }
}
