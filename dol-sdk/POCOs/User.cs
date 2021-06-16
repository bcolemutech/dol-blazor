namespace dol_sdk.POCOs
{
    public interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}