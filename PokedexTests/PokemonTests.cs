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
            var sampleJSON = TestHelpers.GetFileContents("BasicSamplePokemon.json");
            
            var basicInfoController = new PokemonController(TestHelpers.MockRestClient<Pokemon>(HttpStatusCode.OK, sampleJSON));

            var response = basicInfoController.GetBasicInfo("mewtew");

            Assert.AreEqual("mewtew", response.Value.Name);
            Assert.AreEqual("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", response.Value.Description);
            Assert.AreEqual("rare", response.Value.Habitat);
            Assert.IsTrue(response.Value.IsLegendary);
        }
    }
}
