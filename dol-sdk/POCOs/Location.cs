namespace dol_sdk.POCOs;

using Enums;
using Google.Cloud.Firestore;

public interface ILocation
{
    string Name { get; set; }
    string Description { get; set; }
    Place Place { get; set; }
}

[FirestoreData]
public class Location : ILocation
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public Place Place { get; set; }
}
