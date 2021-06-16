using dol_sdk.POCOs;
using Firebase.Auth;

namespace dol_sdk.Services
{
    public interface ISecurityService
    {
        void Login(string user, string password);
        FirebaseAuthLink Identity { get; }
        void Login(IUser user);
    }

    public class SecurityService : ISecurityService
    {
        private readonly IFirebaseAuthProvider _authProvider;
        public FirebaseAuthLink Identity { get; private set; }
        
        public SecurityService(IFirebaseAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public void Login(string user, string password)
        {
            Identity = _authProvider.SignInWithEmailAndPasswordAsync(user, password).Result;
        }

        public void Login(IUser user)
        {
            Login(user.Username, user.Password);
        }
    }
}