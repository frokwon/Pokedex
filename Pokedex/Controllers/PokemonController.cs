using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PokemonController> Logger;
        private readonly IRestClient RestClient;

        public PokemonController(ILogger<PokemonController> logger, IRestClient restClient)
        {
            Logger = logger;
            RestClient = restClient;
        }

        [HttpGet]
        public IEnumerable Get()
        {
            throw new NotImplementedException();
        }
    }
}
