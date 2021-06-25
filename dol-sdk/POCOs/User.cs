using dol_sdk.Enums;

namespace dol_sdk.POCOs
{
    public interface IUser : IPlayerRequest
    {
        string Password { get; set; }
    }

    public class User : IUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Authority Authority { get; set; }
    }
}