namespace dol_sdk.POCOs;

using dol_sdk.Enums;
using Google.Cloud.Firestore;

public interface IUser : IPlayerRequest
{
    string Password { get; set; }
    public string CurrentCharacter { get; set; }
    public string SessionId { get; set; }
    public string ConnectionId { get; set; }
}

[FirestoreData]
public class User : IUser
{
    [FirestoreProperty]
    public string Email { get; set; }

    public string Password { get; set; }

    [FirestoreProperty]
    public string CurrentCharacter { get; set; }

    [FirestoreProperty]
    public string SessionId { get; set; }

    [FirestoreProperty]
    public string ConnectionId { get; set; }

    [FirestoreProperty]
    public Authority Authority { get; set; }
}
