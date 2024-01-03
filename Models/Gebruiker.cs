using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAccessibility.Models;
[Table("Gebruiker")]
public class Gebruiker
{
    public int Gcode { get; set; }
    public string Email { get; set; }
    public string Wachtwoord { get; set; }
    public GebruikerType UserType { get; set; }
}