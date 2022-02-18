namespace dol_sdk.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using dol_sdk.POCOs;
    using dol_sdk.Services;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public interface IAreaController
    {
        Task EditArea(IArea selectedArea);
        Task<IArea> GetArea(int x, int y);
        Task<IEnumerable<IArea>> GetAllAreas();
    }

    public class AreaController : IAreaController
    {
        private const string Bearer = "Bearer";
        private readonly ISecurityService _security;
        private readonly HttpClient _client;
        private readonly string _requestUri;
        public AreaController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService securityService)
        {
            _security = securityService;
            _client = factory.CreateClient();
            _requestUri = configuration["DolApiUri"] + "area";
        }

        public async Task EditArea(IArea selectedArea)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_requestUri}/{selectedArea.X}/{selectedArea.Y}");
            var IdToken = _security.Identity.FirebaseToken;
            
            var contentJson = JsonConvert.SerializeObject(selectedArea);

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            request.Content = new StringContent(contentJson, Encoding.Default, "application/json");
            
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IArea> GetArea(int x, int y)
        {
            var thisUri = new Uri($"{_requestUri}/{x}/{y}");
            var request = new HttpRequestMessage(HttpMethod.Get, thisUri);
            var IdToken = _security.Identity.FirebaseToken;
            
            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            
            var serializer = new JsonSerializer();

            var sr = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<Area>(jsonTextReader);
        }

        public async Task<IEnumerable<IArea>> GetAllAreas()
        {
            var thisUri = new Uri($"{_requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Get, thisUri);
            var IdToken = _security.Identity.FirebaseToken;
            
            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            
            var serializer = new JsonSerializer();

            var sr = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<IEnumerable<Area>>(jsonTextReader);
        }
    }
}
