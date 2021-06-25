namespace dol_sdk.POCOs
{
    using dol_sdk.Enums;

    public interface IPlayerRequest
    {
        string Email { get; }
        Authority Authority { get; }
    }

    public class PlayerRequest : IPlayerRequest
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