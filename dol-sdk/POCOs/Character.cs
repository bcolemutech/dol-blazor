namespace dol_sdk.POCOs
{
    public interface ICharacter
    {
        string Name { get; set; }
        IPosition Position { get; set; }
        IPosition Move { get; set; }
    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public IPosition Position { get; set; }
        public IPosition Move { get; set; }
    }
}