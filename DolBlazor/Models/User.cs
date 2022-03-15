namespace DolBlazor.Models;

using System.ComponentModel.DataAnnotations;
using dol_sdk.Enums;
using dol_sdk.POCOs;

public class User : IUser
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string CurrentCharacter { get; set; }
    public string SessionId { get; set; }
    public string ConnectionId { get; set; }

    [Required]
    [EnumDataType(typeof(Authority))]
    public Authority Authority { get; set; }
}
