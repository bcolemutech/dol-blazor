namespace dol_sdk.POCOs;

using dol_sdk.Enums;
using Google.Cloud.Firestore;

public interface IPosition
{
    int X { get; set; }
    int Y { get; set; }
    string Populace { get; set; }
    string Location { get; set; }
    Action Action { get; set; }
}

[FirestoreData]
public class Position : IPosition
{
    [FirestoreProperty]
    public int X { get; set; }

    [FirestoreProperty]
    public int Y { get; set; }

    [FirestoreProperty]
    public string Populace { get; set; }

    [FirestoreProperty]
    public string Location { get; set; }

    [FirestoreProperty]
    public Action Action { get; set; }
}
