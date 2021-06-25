using System.ComponentModel.DataAnnotations;
using dol_sdk.Enums;
using dol_sdk.POCOs;

namespace DolBlazor.Models
{
    public class User: IUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(Authority))]
        public Authority Authority { get; set; }
    }
}