using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Controllers;
using Microsoft.AspNetCore.Mvc;
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
            var testConfiguration = TestHelpers.GetTestConfiguration();
            
            var basicInfoController = new PokemonController(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), testConfiguration);

            var response = basicInfoController.GetBasicInfo("mewtew");

            var okObjectResult = response as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var pokemon = okObjectResult.Value as Pokemon;
            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.IsTrue(pokemon.IsLegendary);
        }
    }
}
