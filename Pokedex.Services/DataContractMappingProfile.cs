using AutoMapper;

namespace Pokedex.Services
{
    public class DataContractMappingProfile : Profile
    {
        //This should actually be replaced by some infrastructure code that deep copy object into others and extend 
        public DataContractMappingProfile()
        {
            CreateMap<Contract.PokemonBasic, Orchestrators.PokemonBasic>();

            CreateMap<Resources.Contract.PokemonBasic, Contract.PokemonBasic>();
            
            CreateMap<Resources.DataAccess.Pokemon.Contract.PokemonRoot, Resources.Contract.PokemonBasic>();
        }
    }
}
