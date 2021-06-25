using System;
using System.Threading.Tasks;
using dol_sdk.Enums;
using dol_sdk.POCOs;
using Firebase.Auth;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;

namespace dol_sdk.Services
{
    public interface ISecurityService
    {
        Task Login(string user, string password);
        FirebaseAuthLink Identity { get; }
        Authority Authority { get; }
        Task Login(IUser user);
        public event Notify IdentityUpdated;
        void Logout();
    }

    public class SecurityService : ISecurityService
    {
        private readonly IFirebaseAuthProvider _authProvider;
        private readonly IConfiguration _configuration;
        
        public FirebaseAuthLink Identity { get; private set; }
        public Enums.Authority Authority { get; private set; }
        
        public event Notify IdentityUpdated;
        
        public SecurityService(IFirebaseAuthProvider authProvider, IConfiguration configuration)
        {
            _authProvider = authProvider;
            _configuration = configuration;
            Authority = Authority.Player;
        }

        public async Task Login(string user, string password)
        {
            Identity = await _authProvider.SignInWithEmailAndPasswordAsync(user, password);

            IJsonSerializer serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
    
            var payload = decoder.DecodeToObject(Identity.FirebaseToken, _configuration["FirebaseApiKey"], false);
            Authority = (Authority) Convert.ToInt32(payload["Authority"]);
            
            IdentityUpdated?.Invoke();
        }

        public async Task Login(IUser user)
        {
            await Login(user.Email, user.Password);
        }

        public void Logout()
        {
            Identity = null;
            Authority = Authority.Player;
            IdentityUpdated?.Invoke();
        }
    }

    public delegate void Notify();
}