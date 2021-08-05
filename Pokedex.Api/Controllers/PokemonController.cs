using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pokedex.Api.Clients;
using Pokedex.Api.Framework;
using System;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonInformationServiceClient pokemonInformationServiceClient;
        private readonly ICacheService cacheService;

        public PokemonController(IPokemonInformationServiceClient pokemonInformationServiceClient,             
                                 ICacheService cacheService)
        {
            this.pokemonInformationServiceClient = pokemonInformationServiceClient;
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            Task<string> retrievePokemonWithNoNameInfoTask = TaskGenerationHelper.CreateTask(@"You need to choose a pokemon name, and add it to the URL: currentURL + ""/pikachu");

            string result = await retrievePokemonWithNoNameInfoTask;

            return Ok(result);
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<Models.PokemonBasic>> Get(string pokemonName)
        {            
            string cacheKey = $"{ InternalDataContracts.CacheKeys.PokemonBasicCacheKeyPreffix }{ pokemonName }";

            Models.PokemonBasic pokemonBasicCacheValue = cacheService.TryGetValue<Models.PokemonBasic>(cacheKey);

            if (pokemonBasicCacheValue == null)
            {
                Models.PokemonBasic retrievePokemonFromService = await pokemonInformationServiceClient.GetPokemonDetailsAsync(pokemonName);

                if (retrievePokemonFromService == null)
                    return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");
                
                cacheService.Set(cacheKey, retrievePokemonFromService);

                return Ok(retrievePokemonFromService);
            }

            return Ok(pokemonBasicCacheValue);
        }
    }
}
