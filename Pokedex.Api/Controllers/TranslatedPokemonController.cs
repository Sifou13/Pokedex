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

        public TranslatedPokemonController(Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator, IMapper mapper)
        {
            this.pokemonInformationOrchestrator = pokemonInformationOrchestrator;
            this.mapper = mapper;
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
            Services.Contract.PokemonBasic result;

            result = await pokemonInformationOrchestrator.GetTranslatedPokemonDetailsAsync(pokemonName);
            
            if (result == null)
                return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");

            return Ok(mapper.Map<Models.PokemonBasic>(result));
        }
    }
}
