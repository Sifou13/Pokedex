using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Framework;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator;
        private readonly IMapper mapper;

        public PokemonController(Services.Contract.Orchestrators.IPokemonInformationOrchestrator pokemonInformationOrchestrator, IMapper mapper)
        {
            this.pokemonInformationOrchestrator = pokemonInformationOrchestrator;
            this.mapper = mapper;
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
            Services.Contract.PokemonBasic retrievePokemonFromService = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonName);

            if (retrievePokemonFromService == null)
                return NotFound($"We could not find a pokemon named {pokemonName}; it could be an issue on our end, so if you're convinced something went wrong, please get in touch");

            return Ok(mapper.Map<Models.PokemonBasic>(retrievePokemonFromService));
        }
    }
}
