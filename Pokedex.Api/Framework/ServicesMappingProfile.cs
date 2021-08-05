using AutoMapper;
using System;

namespace Pokedex.Api.Framework
{
    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {            
            //From Grpc Service Message to Model Api
            CreateMap<Pokedex.Clients.PokemonBasic, Models.PokemonBasic>();
        }
    }
}
