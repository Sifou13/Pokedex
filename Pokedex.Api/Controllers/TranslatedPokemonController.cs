using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Framework;
using System;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/pokemon/translated/")]
    public class TranslatedPokemonController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            string result = await TaskGenerationHelper.CreateTask(@"This API is designed around individual Pokemons. Choose a pokemon name to see a fun translation of their description. You can do this by adding it to the URL: currentURL + ""/mewtwo");

            return Ok(result);
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<string>> Get(string pokemonName)
        {   
            string result = await TaskGenerationHelper.CreateTask($"Sorry, {pokemonName}'s information have not yet been made available.....so, we can't yet translate their desription, bare with us.");

            return Ok(result);
        }
    }
}
