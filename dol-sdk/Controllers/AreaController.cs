﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dol_sdk.POCOs;
using dol_sdk.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace dol_sdk.Controllers
{
    public interface IAreaController
    {
        Task EditArea(IArea selectedArea);
        Task<IArea> GetArea(int x, int y);
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
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            
            var serializer = new JsonSerializer();

            var sr = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<Area>(jsonTextReader);
        }
    }
}