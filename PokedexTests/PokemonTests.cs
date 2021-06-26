using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Controllers;
using PokemonServices.Models;
using System.Net;

namespace PokedexTests
{
    [TestClass]
    public class PokemonTests
    {
        [TestMethod]
        public void GetBasicInfoValidPokemon()
        {
            var basicInfoController = new PokemonController(TestHelpers.MockRestClient<Pokemon>(HttpStatusCode.OK, "{ \"name\" : \"mewtew\", \"description\" : \"test description\", \"habitat\" : \"my habitat\", \"isLegendary\" : true }"));

            var response = basicInfoController.GetBasicInfo("mewtew");

            Assert.AreEqual("mewtew", response.Value.Name);
            Assert.AreEqual("test description", response.Value.Description);
            Assert.AreEqual("my habitat", response.Value.Habitat);
            Assert.IsTrue(response.Value.IsLegendary);
        }
    }
}
