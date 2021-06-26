using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonServices.Models
{
    public class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("habitat")]
        public string Habitat { get; set; }
        [JsonProperty("isLegendary")]
        public bool IsLegendary { get; set; }
    }
}
