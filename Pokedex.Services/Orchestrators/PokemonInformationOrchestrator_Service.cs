using AutoMapper;
using Grpc.Core;
using Pokedex.Services.Contract.Orchestrators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Services.Orchestrators
{
    public class PokemonInformationOrchestrator_GrpcService : IPokemonInformationService.IPokemonInformationServiceBase
    {
        private readonly IPokemonInformationOrchestrator pokemonInformationOrchestrator;
        private readonly IMapper mapper;

        public PokemonInformationOrchestrator_GrpcService(IPokemonInformationOrchestrator pokemonInformationOrchestrator, IMapper mapper)
        {
            this.pokemonInformationOrchestrator = pokemonInformationOrchestrator;
            this.mapper = mapper;
        }

        public override async Task<Orchestrators.PokemonBasic> GetPokemonDetails(PokemonRequest pokemonRequest, ServerCallContext serverCallContext)
        {
            Contract.PokemonBasic pokemonBasic = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonRequest.PokemonName);

            return mapper.Map<Orchestrators.PokemonBasic>(pokemonBasic);
        }

        public override async Task<Orchestrators.PokemonBasic> GetTranslatedPokemonDetails(PokemonRequest pokemonRequest, ServerCallContext serverCallContext)
        {
            Contract.PokemonBasic pokemonBasic = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonRequest.PokemonName);

            return mapper.Map<Orchestrators.PokemonBasic>(pokemonBasic);
        }
    }
}
