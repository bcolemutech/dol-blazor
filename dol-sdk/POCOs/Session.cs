namespace dol_sdk.POCOs;

using System.Collections.Generic;
using Google.Cloud.Firestore;

public interface ISession
{
    public string ID { get; set; }
    public Position Position { get; set; }
    public ICollection<User> Players { get; set; }
}

[FirestoreData]
public class Session : ISession
{
    [FirestoreProperty]
    public string ID { get; set; }

    [FirestoreProperty]
    public Position Position { get; set; }

    [FirestoreProperty]
    public ICollection<User> Players { get; set; }
}
