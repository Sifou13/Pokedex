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
        private readonly Services.Contract.IPokemonInformationOrchestrator pokemonInformationOrchestrator;
        private readonly IMapper mapper;

        public PokemonController(Services.Contract.IPokemonInformationOrchestrator pokemonInformationOrchestrator, IMapper mapper)
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
        public async Task<ActionResult<string>> Get(string pokemonName)
        {
            Services.Contract.PokemonBasic retrievePokemonFromService = await pokemonInformationOrchestrator.GetPokemonDetailsAsync(pokemonName); 

            Models.PokemonBasic result = mapper.Map<Models.PokemonBasic>(retrievePokemonFromService);

            return Ok(result);
        }
    }
}
