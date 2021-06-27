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
        
        [HttpGet]
        [Route("pokemon/translated/{name}")]
        public ActionResult<Pokemon> GetTranslatedInfo(string name)
        {
            throw new NotImplementedException();
        }
    }
}
