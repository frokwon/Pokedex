using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokemonServices.Models;
using RestSharp;
using System;
using System.Net;

namespace PokemonServices
{
    public interface IPokemonService
    {
        Pokemon GetBasicInfo(string name);
        Pokemon GetTranslatedInfo(string name);
    }
    
    public class PokemonService : IPokemonService
    {
        private readonly IRestClient RestClient;
        private readonly IConfiguration Configuration;
        private readonly ITranslationService TranslationService;
        public PokemonService(IRestClient restClient, IConfiguration configuration, ITranslationService translationService)
        {
            RestClient = restClient;
            Configuration = configuration;
            TranslationService = translationService;
        }
        
        /// <summary>
        /// Gets basic information for a supplied pokemon name
        /// </summary>
        /// <param name="name">Name of the pokemon</param>
        /// <returns>Basic information in JSON format</returns>
        public Pokemon GetBasicInfo(string name)
        {
            var endPoint = Configuration["Endpoints:PokeApi"];
            RestClient.BaseUrl = new Uri($"{endPoint}/{name}");

            var response = RestClient.Execute<dynamic>(new RestRequest(Method.GET));

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            
            var pokemon = new Pokemon
            {
                Name = content.name,
                Habitat = content.habitat.name,
                IsLegendary = content.is_legendary,
                Description = content.flavor_text_entries[0].flavor_text
            };

            pokemon.Description = pokemon.Description.Replace("\n", " ").Replace("\f", " ");

            return pokemon;
        }
        /// <summary>
        /// Gets translated information for a supplied pokemon name
        /// </summary>
        /// <param name="name">Name of the pokemon</param>
        /// <returns>Translated information in JSON format</returns>
        public Pokemon GetTranslatedInfo(string name)
        {
            var pokemon = GetBasicInfo(name);

            if (pokemon == null)
                return null;
 
            if (pokemon.Habitat == "cave" || pokemon.IsLegendary)
                pokemon.Description = TranslationService.Translate(pokemon.Description, "yoda");
            else
                pokemon.Description = TranslationService.Translate(pokemon.Description, "shakespeare");

            return pokemon;
        }
    }
}
