using System.ComponentModel.DataAnnotations;
using dol_sdk.Enums;
using dol_sdk.POCOs;

namespace DolBlazor.Models
{
    public class Area: IArea
    {
        public Area()
        {
            Region = "The Great Sea";
            Description = "Open ocean.";
            IsCoastal = false;
            Ecosystem = Ecosystem.Ocean;
            Navigation = Navigation.Impassable;
        }

        public Area(IArea area)
        {
            X = area.X;
            Y = area.Y;
            Region = area.Region;
            Description = area.Description;
            IsCoastal = area.IsCoastal;
            Ecosystem = area.Ecosystem;
            Navigation = area.Navigation;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        [StringLength(30)]
        public string Region { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public bool IsCoastal { get; set; }
        
        [Required]
        [EnumDataType(typeof(Ecosystem))]
        public Ecosystem Ecosystem { get; set; }
        
        [Required]
        [EnumDataType(typeof(Navigation))]
        public Navigation Navigation { get; set; }
    }
}
