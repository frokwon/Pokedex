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
    public class TranslationTests
    {
        private IConfiguration Configuration;
        
        [TestInitialize]
        public void TestInitialize()
        {
            Configuration = TestHelpers.GetTestConfiguration();
        }
        
        [TestMethod]
        public void YodaTranslation()
        {
            var sampleJSON = TestHelpers.GetFileContents("YodaSample.json");
           
            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration);

            var translation = translationService.Translate("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", "yoda");

            Assert.AreEqual("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", translation);
        }
        [TestMethod]
        public void ShakespeareTranslation()
        {
            var sampleJSON = TestHelpers.GetFileContents("ShakespeareSample.json");

            var translationService = new TranslationService(TestHelpers.MockRestClient<dynamic>(HttpStatusCode.OK, sampleJSON), Configuration);

            var translation = translationService.Translate("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", "shakespeare");

            Assert.AreEqual("'t wast did create by a scientist after years of horrific gene splicing and dna engineering experiments.", translation);
        }
    }
}
