namespace dol_sdk.POCOs;

using Google.Cloud.Firestore;

public interface IPopulace
{
    string Name { get; set; }
    string Description { get; set; }
    int Size { get; set; }
    bool HasPort { get; set; }
}

[FirestoreData]
public class Populace : IPopulace
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public int Size { get; set; }

    [FirestoreProperty]
    public bool HasPort { get; set; }
}
