using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonServices
{
    public class TranslationService : ITranslationService
    {
        private readonly IRestClient RestClient;
        private readonly IConfiguration Configuration;
        public TranslationService(IRestClient restClient, IConfiguration configuration)
        {
            RestClient = restClient;
            Configuration = configuration;

        }
        /// <summary>
        /// Translates text using FunTranslation's apis
        /// </summary>
        /// <param name="text">The text that you wish to translate</param>
        /// <param name="type">The type of text that you wish to translation - this will append it to the URL endpoint</param>
        /// <returns>Translated text</returns>
        public string Translate(string text, string type)
        {
            try
            {
                var endPoint = Configuration["Endpoints:FunTranslationsApi"];

                RestClient.BaseUrl = new Uri($"{endPoint}{type}");
                var request = new RestRequest(Method.POST);
                request.AddParameter("text", text);
                var response = RestClient.Execute<dynamic>(request);
                var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
                return content.contents.translated;
            }
            catch(Exception)
            {
                // if translation fails, then return the original text
                return text;
            }
        }
    }
}
