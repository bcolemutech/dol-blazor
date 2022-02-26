namespace dol_sdk.POCOs;

using dol_sdk.Enums;

public interface IUser : IPlayerRequest
{
    string Password { get; set; }
    public ICharacter CurrentCharacter { get; set; }
    public string SessionId { get; set; }
    public string ConnectionId { get; set; }
}

public class User : IUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public ICharacter CurrentCharacter { get; set; }
    public string SessionId { get; set; }
    public string ConnectionId { get; set; }
    public Authority Authority { get; set; }
}
