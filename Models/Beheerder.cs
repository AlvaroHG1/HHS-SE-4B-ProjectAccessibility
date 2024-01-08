using Newtonsoft.Json;

namespace ProjectAccessibility.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Beheerder")]
public class Beheerder : Gebruiker
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string Rol { get; set; }
}