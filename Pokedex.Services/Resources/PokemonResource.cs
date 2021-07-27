using AutoMapper;

namespace Pokedex.Services.Resources
{
    public class PokemonResource : Contract.IPokemonResource
    {
        private readonly DataAccess.IPokemonDA pokemonDA;
        private readonly IMapper mapper;

        public PokemonResource(DataAccess.IPokemonDA pokemonDA, IMapper mapper)
        {
            this.pokemonDA = pokemonDA;
            this.mapper = mapper;
        }

        public Contract.PokemonBasic SelectByName(string name)
        {
            DataAccess.PokemonBasic retrievedPokemon = pokemonDA.SelectByName(name);

           return mapper.Map<Resources.Contract.PokemonBasic>(retrievedPokemon);
        }
    }
}
