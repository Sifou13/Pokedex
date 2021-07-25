﻿using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Framework;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
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
            Task<string> retrievePokemonFromService = TaskGenerationHelper.CreateTask($"Sorry, {pokemonName}'s information have not yet been made available.....");

            string result = await retrievePokemonFromService;

            return Ok(result);
        }
    }
}
