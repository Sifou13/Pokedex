using AutoMapper;

namespace Pokedex.Services
{
    public class DataContractMappingProfile : Profile
    {
        public DataContractMappingProfile()
        {
            CreateMap<Contract.PokemonBasic, Resources.Contract.PokemonBasic>().ReverseMap();
            CreateMap<Contract.PokemonHabitat, Resources.Contract.PokemonHabitat>().ReverseMap();

            CreateMap<Resources.Contract.PokemonBasic, Resources.DataAccess.Pokemon.Contract.PokemonRoot>().ReverseMap();
            CreateMap<Resources.Contract.PokemonHabitat, Resources.DataAccess.Pokemon.Contract.PokemonHabitat>().ReverseMap();
        }
    }
}
