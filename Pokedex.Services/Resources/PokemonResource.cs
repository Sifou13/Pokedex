using AutoMapper;
using System.Linq;

namespace Pokedex.Services.Resources
{
    public class PokemonResource : Contract.IPokemonResource
    {
        private readonly DataAccess.Pokemon.IPokemonDA pokemonDA;
        
        public PokemonResource(DataAccess.Pokemon.IPokemonDA pokemonDA)
        {
            this.pokemonDA = pokemonDA;
        }

        //While here a resource can look like an additional, not much of use layer
        //it is becoming useful when for one use case, we need to aggregate data in a specific way,
        //or to act, for example as Data layer orchestrator - can be seen as a join        
        public Contract.PokemonBasic SelectByName(string name)
        {
            DataAccess.Pokemon.PokemonRoot retrievedPokemonRoot = pokemonDA.SelectByName(name).Result;

            if (retrievedPokemonRoot == null)
                return null;
            
            if( retrievedPokemonRoot.Species?.Url == null)
                return new Contract.PokemonBasic
                {
                    Name = retrievedPokemonRoot.Name,
                    Description = "Could not be loaded...",
                };

            //Here we retrieve the species using the url provided in the previous Api call request's response so
            //that we only have to maintain the entry point in config and allow for API structure changes at the provider/third party's end 

            DataAccess.Pokemon.PokemonSpecies species = pokemonDA.SelectSpeciesByUrl(retrievedPokemonRoot.Species.Url).Result;

            //Here we can see by hardcoding this property here for this demo, that if we were a product manager takig the 
            // product to the next level, we could introduce another endpoint taking a language as well and returning the first
            // corresponding or add criterial (color I believe it was)
            return new Contract.PokemonBasic
            {
                Name = retrievedPokemonRoot.Name,
                Description = species.Flavor_Text_Entries.FirstOrDefault(x => x.Language.Name == "en")?.Flavor_Text,
                Habitat = (Contract.PokemonHabitat)species.Habitat.Name,
                IsLegendary = species.Is_Legendary
           }; 
        }
    }
}
