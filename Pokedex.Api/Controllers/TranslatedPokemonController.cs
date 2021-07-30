using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Framework;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/pokemon/translated/")]
    public class TranslatedPokemonController : ControllerBase
    {
        private readonly Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public TranslatedPokemonController(Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator, 
                                           IMapper mapper,
                                           ICacheService cacheService)
        {
            this.pokemonInformationOrchestrator = pokemonInformationOrchestrator;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            string result = await TaskGenerationHelper.CreateTask(@"This API is designed around individual Pokemons. Choose a pokemon name to see a fun translation of their description. You can do this by adding it to the URL: currentURL + ""/mewtwo");

            return Ok(result);
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<Models.PokemonBasic>> Get(string pokemonName)
        {
            string translationCacheKey = $"{ InternalDataContracts.CacheKeys.TranslatedPokemonCacheKeyPreffix}{ pokemonName }";

            Models.PokemonBasic translatedPokemonBasicCacheValue = cacheService.TryGetValue<Models.PokemonBasic>(translationCacheKey);

            if (translatedPokemonBasicCacheValue == null)
            {
                Services.Contract.PokemonBasic translatedPokemonFromService = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);

                if (translatedPokemonFromService == null)
                    return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");

                Models.PokemonBasic returnValue = mapper.Map<Models.PokemonBasic>(translatedPokemonFromService);

                cacheService.Set(translationCacheKey, returnValue);

                return Ok(returnValue);
            }
            
            return Ok(translatedPokemonBasicCacheValue);
        }
    }
}
