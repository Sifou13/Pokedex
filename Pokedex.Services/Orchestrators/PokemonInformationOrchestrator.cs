using AutoMapper;
using Pokedex.Services.Contract.Orchestrators;
using Pokedex.Services.Resources.Contract;
using System.Threading.Tasks;

//To explain this layer - Using the IDesign Methodology, this layer called Orchestrator/Manager namespace
// will contain the main business logic behind the service, and will orchestrate a number of calls to
// different resources (if this became the requirement) while handling the use case
// this allow also for greater testability of the whole flow, mocking only third parties that have overhead
// to be tested in isolation, so that the "CRUD" is only tested once
//The Mapping (AutoMapper) is being tested as part of the flow, and this is a technical decision, or a trade-off when using IDesign
// but it does actually test that developpers have added the correct mapping the profile maps.
namespace Pokedex.Services.Orchestrators
{
    public class PokemonInformationOrchestrator : IPokemonInformationOrchestrator
    {
        private readonly IPokemonResource pokemonResource;
        private readonly ITranslationResource translationResource;
        private readonly IMapper mapper;

        public PokemonInformationOrchestrator(IPokemonResource pokemonResource, ITranslationResource translationResource, IMapper mapper)
        {
            this.pokemonResource = pokemonResource;
            this.translationResource = translationResource;
            this.mapper = mapper;
        }
                
        public async Task<Contract.PokemonBasic> GetPokemonDetailsAsync(string pokemonName)
        {
            Resources.Contract.PokemonBasic retrievedPokemonBasic = pokemonResource.SelectByName(pokemonName);

            return await Task.FromResult(mapper.Map<Contract.PokemonBasic>(retrievedPokemonBasic));
        }

        //This class shows how the IDesign Methodology helps is decomposing the logical services in layer while applying some rules 
        // to avoid circular references when the application grow and showing all the use case/requirement being in the code below 
        public async Task<Contract.PokemonBasic> GetTranslatedPokemonDetailsAsync(string pokemonName)
        {
            Resources.Contract.PokemonBasic retrievedPokemonBasic = pokemonResource.SelectByName(pokemonName);

            if (retrievedPokemonBasic == null)
                return null;

            bool isEligibleForYodaTranslation = retrievedPokemonBasic.Habitat == Resources.Contract.PokemonHabitat.Cave || retrievedPokemonBasic.IsLegendary;

            retrievedPokemonBasic.Description = TranslateByPokemonEligibility(isEligibleForYodaTranslation, retrievedPokemonBasic.Description) ?? retrievedPokemonBasic.Description;

            return await Task.FromResult(mapper.Map<Contract.PokemonBasic>(retrievedPokemonBasic));
        }

        private string TranslateByPokemonEligibility(bool ElibibleForYodaTranslation, string pokemonDescription)
        {
            if (ElibibleForYodaTranslation)
                return translationResource.TranslateUsingYoda(pokemonDescription);

            return translationResource.TranslateUsingShakespeare(pokemonDescription);
        }
    }
}
