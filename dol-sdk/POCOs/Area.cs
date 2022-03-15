namespace dol_sdk.POCOs;

using Enums;
using Google.Cloud.Firestore;

public interface IArea
{
    int X { get; set; }
    int Y { get; set; }
    string Region { get; set; }
    string Description { get; set; }
    bool IsCoastal { get; set; }
    Ecosystem Ecosystem { get; set; }
    Navigation Navigation { get; set; }
}

[FirestoreData]
public class Area : IArea
{
    [FirestoreProperty]
    public int X { get; set; }

    [FirestoreProperty]
    public int Y { get; set; }

    [FirestoreProperty]
    public string Region { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public bool IsCoastal { get; set; }

    [FirestoreProperty]
    public Ecosystem Ecosystem { get; set; }

    [FirestoreProperty]
    public Navigation Navigation { get; set; }
}
