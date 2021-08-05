using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Clients;
using Pokedex.Api.Framework;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/pokemon/translated/")]
    public class TranslatedPokemonController : ControllerBase
    {
        private readonly IPokemonInformationServiceClient pokemonInformationServiceClient;
        private readonly ICacheService cacheService;

        public TranslatedPokemonController(IPokemonInformationServiceClient pokemonInformationServiceClient, 
                                           ICacheService cacheService)
        {
            this.pokemonInformationServiceClient = pokemonInformationServiceClient;
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
                Models.PokemonBasic translatedPokemonFromService = await pokemonInformationServiceClient.GetTranslatedPokemonDetailsAsync(pokemonName);

                if (translatedPokemonFromService == null)
                    return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");
                                
                cacheService.Set(translationCacheKey, translatedPokemonFromService);

                return Ok(translatedPokemonFromService);
            }
            
            return Ok(translatedPokemonBasicCacheValue);
        }
    }
}
