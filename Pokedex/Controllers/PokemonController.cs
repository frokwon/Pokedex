using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokemonServices;
using PokemonServices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]

    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService PokemonService;
        public PokemonController(IPokemonService pokemonService)
        {
            PokemonService = pokemonService;
        }
        /// <summary>
        /// Retrieve basic information for a Pokemon character
        /// </summary>
        /// <remarks>Retrieve basic information for a Pokemon character</remarks>
        /// <param name="name">The name of the Pokemon character</param>
        /// <response code="200">Returns basic information about the character</response>
        /// <response code="404">If no information was found</response>
        /// <returns></returns>
        [HttpGet]
        [Route("pokemon/{name}")]
        public IActionResult GetBasicInfo(string name)
        {
            try
            {
                var pokemon = PokemonService.GetBasicInfo(name);

                if (pokemon == null)
                    return NotFound("Pokemon not found");

                return Ok(pokemon);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Retrieve translated information for a Pokemon character
        /// </summary>
        /// <remarks>If the character's habitat is "cave" or the character is legendary, then the yoda translation is applied, otherwise the Shakespeare translation is applied</remarks>
        /// <param name="name">The name of the Pokemon character</param>
        /// <response code="200">Returns translated information about the character</response>
        /// <response code="404">If no information was found</response>
        /// <returns></returns>
        [HttpGet]
        [Route("pokemon/translated/{name}")]
        public ActionResult<Pokemon> GetTranslatedInfo(string name)
        {
            try
            {
                var pokemon = PokemonService.GetTranslatedInfo(name);

                if (pokemon == null)
                    return NotFound("Pokemon not found");

                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
