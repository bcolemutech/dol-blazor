using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using dol_sdk.Enums;
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
        private readonly string _requestUri;
        public AdminController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _security = security;
            _client = factory.CreateClient();
            _requestUri = configuration["DolApiUri"] + "user";
        }
        
        private string IdToken => _security.Identity.FirebaseToken;

        public async Task UpdateUser(IUser user)
        {
            var content = new PlayerRequest(user.Username, user.Authority);

            var contentJson = JsonConvert.SerializeObject(content); 

            var request = new HttpRequestMessage(HttpMethod.Post, _requestUri);

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            request.Content = new StringContent(contentJson);
            
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }

    public readonly struct PlayerRequest
    {
        public PlayerRequest(string email, Authority authority)
        {
            Email = email;
            Authority = authority;
        }

        public string Email { get; }
        public Authority Authority { get; }
    }
}
