namespace dol_sdk.POCOs;

using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;
using Action = dol_sdk.Enums.Action;

public interface ICharacter
{
    string Name { get; set; }
    Position Position { get; set; }
}

[FirestoreData]
public class Character : ICharacter
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty(ConverterType = typeof(PositionConverter))]
    public Position Position { get; set; }
}

public class PositionConverter : IFirestoreConverter<Position>
{
    public object ToFirestore(Position value)
    {
        return new Position
        {
            X = value.X,
            Y = value.Y,
            Location = value.Location,
            Populace = value.Populace,
            Action = value.Action
        };
    }

    public Position FromFirestore(object value)
    {
        if (value is IDictionary<string, object> map)
        {
            return new Position
            {
                Action = (Action)Convert.ToInt32(map["Action"]),
                Location = (string)map["Location"],
                Populace = (string)map["Populace"],
                X = Convert.ToInt32(map["X"]),
                Y = Convert.ToInt32(map["Y"])
            };
        }

        throw new ArgumentException($"Unexpected data: {value.GetType()}");
    }
}
