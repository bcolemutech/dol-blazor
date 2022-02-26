namespace dol_sdk.POCOs;

using System.Collections.Generic;

public interface ISession
{
    public string ID { get; set; }
    public IPosition Position { get; set; }
    public ICollection<IUser> Players { get; set; }
}

public class Session : ISession
{
    public string ID { get; set; }
    public IPosition Position { get; set; }
    public ICollection<IUser> Players { get; set; }
}
