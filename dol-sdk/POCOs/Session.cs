namespace dol_sdk.POCOs;

using System.Collections.Generic;
using Google.Cloud.Firestore;

public interface ISession
{
    public string ID { get; set; }
    public IPosition Position { get; set; }
    public ICollection<IUser> Players { get; set; }
}

[FirestoreData]
public class Session : ISession
{
    [FirestoreProperty]
    public string ID { get; set; }

    [FirestoreProperty]
    public IPosition Position { get; set; }

    [FirestoreProperty]
    public ICollection<IUser> Players { get; set; }
}
