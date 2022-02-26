namespace dol_sdk.POCOs;

using dol_sdk.Enums;

public interface IPosition
{
    int X { get; set; }
    int Y { get; set; }
    string Populace { get; set; }
    string Location { get; set; }
    Action Action { get; set; }
}

public class Position : IPosition
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Populace { get; set; }
    public string Location { get; set; }
    public Action Action { get; set; }
}
