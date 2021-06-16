using System.ComponentModel.DataAnnotations;
using dol_sdk.POCOs;

namespace DolBlazor.Models
{
    public class User: IUser
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}