using System;
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
    public interface IAdminController
    {
        Task UpdateUser(IUser user);
    }

    public class AdminController : IAdminController
    {
        private const string Bearer = "Bearer";
        private readonly ISecurityService _security;
        private readonly HttpClient _client;
        private readonly Uri _requestUri;

        public AdminController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _security = security;
            _client = factory.CreateClient();
            _requestUri = new Uri(configuration["DolApiUri"] + "user");
        }

        private string IdToken => _security.Identity.FirebaseToken;

        public async Task UpdateUser(IUser user)
        {
            var content = new PlayerRequest(user.Email, user.Authority);

            var contentJson = JsonConvert.SerializeObject(content);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _requestUri,
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue(Bearer, IdToken)
                },
                Content = new StringContent(contentJson, Encoding.Default, "application/json")
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}