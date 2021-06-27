using System.ComponentModel.DataAnnotations;
using dol_sdk.Enums;
using dol_sdk.POCOs;

namespace DolBlazor.Models
{
    public class Area: IArea
    {
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