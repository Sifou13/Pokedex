using AutoMapper;
using System;

namespace Pokedex.Api.Framework
{
    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {
            //not reversed since only needing one way for now
            CreateMap<Pokedex.Services.Contract.PokemonBasic, Models.PokemonBasic>();

            //When Setting Cache
            CreateMap<Pokedex.Services.Contract.PokemonBasic, Models.PokemonBasic>();
        }
    }
}
