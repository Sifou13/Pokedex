using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pokedex.Api.Framework;
using System;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public PokemonController(Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator,             
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
                Services.Contract.PokemonBasic retrievePokemonFromService = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonName);

                if (retrievePokemonFromService == null)
                    return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");
                
                cacheService.Set(cacheKey, mapper.Map<Models.PokemonBasic>(retrievePokemonFromService));

                return Ok(mapper.Map<Models.PokemonBasic>(retrievePokemonFromService));
            }

            return Ok(mapper.Map<Models.PokemonBasic>(pokemonBasicCacheValue));
        }
    }
}
