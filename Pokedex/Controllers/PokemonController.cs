using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonServices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IRestClient RestClient;

        public PokemonController(IRestClient restClient)
        {
            RestClient = restClient;
        }

        [HttpGet]
        public ActionResult<Pokemon> GetBasicInfo(string name)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public ActionResult<Pokemon> GetTranslatedInfo(string name)
        {
            throw new NotImplementedException();
        }
    }
}
