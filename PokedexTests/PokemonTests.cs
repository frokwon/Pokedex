using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Controllers;
using Microsoft.AspNetCore.Mvc;
using PokemonServices.Models;
using System.Net;
using System;
using PokemonServices;
using Microsoft.Extensions.Configuration;

namespace PokedexTests
{
    [TestClass]
    public class PokemonTests
    {
        private IConfiguration Configuration;
        
        [TestInitialize]
        public void TestInitialize()
        {
            Configuration = TestHelpers.GetTestConfiguration();
        }
        
        [TestMethod]
        public void GetBasicInfoValidPokemon()
        {
            var sampleJSON = TestHelpers.GetFileContents("BasicSamplePokemonLegendary.json");
            
            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, ""), Configuration);
                
            var pokemonService = new PokemonService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration, translationService);

            var pokemon = pokemonService.GetBasicInfo("mewtew");

            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.IsTrue(pokemon.IsLegendary);
        }

        [TestMethod]
        public void GetBasicInfoInValidPokemon()
        {
            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, ""), Configuration);
            var pokemonService = new PokemonService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.NotFound, string.Empty), Configuration, translationService);

            var pokemon = pokemonService.GetBasicInfo("mewtew1111");
            Assert.IsNull(pokemon);
        }
        [TestMethod]
        public void GetTranslatedInfoLegendary()
        {
            var sampleJSON = TestHelpers.GetFileContents("BasicSamplePokemonLegendary.json");
            var sampleYodaJSON = TestHelpers.GetFileContents("YodaSample.json");

            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleYodaJSON), Configuration);

            var pokemonService = new PokemonService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration, translationService);

            var pokemon = pokemonService.GetTranslatedInfo("mewtew");
            
            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.IsTrue(pokemon.IsLegendary);
        }
        [TestMethod]
        public void GetTranslatedNonLegendaryCave()
        {
            var sampleJSON = TestHelpers.GetFileContents("BasicSamplePokemonNonLegendaryCave.json");
            var sampleYodaJSON = TestHelpers.GetFileContents("YodaSample.json");

            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleYodaJSON), Configuration);

            var pokemonService = new PokemonService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration, translationService);

            var pokemon = pokemonService.GetTranslatedInfo("mewtew");

            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", pokemon.Description);
            Assert.AreEqual("cave", pokemon.Habitat);
            Assert.IsFalse(pokemon.IsLegendary);
        }
        [TestMethod]
        public void GetTranslatedOther()
        {
            var sampleJSON = TestHelpers.GetFileContents("BasicSamplePokemonOther.json");
            var sampleShakespeareJSON = TestHelpers.GetFileContents("ShakespeareSample.json");

            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleShakespeareJSON), Configuration);

            var pokemonService = new PokemonService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration, translationService);

            var pokemon = pokemonService.GetTranslatedInfo("mewtew");

            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("'t wast did create by a scientist after years of horrific gene splicing and dna engineering experiments.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.IsFalse(pokemon.IsLegendary);
        }
    }
}
