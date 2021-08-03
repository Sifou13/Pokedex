using AutoMapper;

namespace Pokedex.Services
{
    public class DataContractMappingProfile : Profile
    {
        //This should actually be replaced by some infrastructure code that deep copy object into others and extend 
        public DataContractMappingProfile()
        {
            CreateMap<Contract.PokemonBasic, Resources.Contract.PokemonBasic>().ReverseMap();
            CreateMap<Contract.PokemonHabitat, Resources.Contract.PokemonHabitat>().ReverseMap();

            CreateMap<Resources.Contract.PokemonBasic, Resources.DataAccess.Pokemon.Contract.PokemonRoot>().ReverseMap();
            CreateMap<Resources.Contract.PokemonHabitat, Resources.DataAccess.Pokemon.Contract.PokemonHabitat>().ReverseMap();
        }
    }
}
