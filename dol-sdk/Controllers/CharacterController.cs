using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using dol_sdk.POCOs;
using dol_sdk.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace dol_sdk.Controllers
{
    public interface ICharacterController
    {
        Character Current { get; set; } 
        IEnumerable<Character> GetCharacterData();
        void Delete(string id);
        void CreateCharacter(Character newCharacter);
    }

    public class CharacterController : ICharacterController
    {
        private const string Bearer = "Bearer";
        private readonly ISecurityService _security;
        private readonly HttpClient _client;
        private readonly string _requestUri;

        public CharacterController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _security = security;
            _client = factory.CreateClient();
            _requestUri = configuration["DolApiUri"] + "character";
        }

        public Character Current { get; set; }

        public IEnumerable<Character> GetCharacterData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _requestUri);
            var IdToken = _security.Identity.FirebaseToken;

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);

            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStreamAsync().Result;
            
            var serializer = new JsonSerializer();

            var sr = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<IEnumerable<Character>>(jsonTextReader);
        }

        public void Delete(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_requestUri}/{id}");
            var IdToken = _security.Identity.FirebaseToken;

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        public void CreateCharacter(Character newCharacter)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_requestUri}/{newCharacter.Name}");
            var IdToken = _security.Identity.FirebaseToken;

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}