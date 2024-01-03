namespace ProjectAccessibility.Models;

public class Beheerder : Gebruiker
{
    public Gebruiker Gebruiker;
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string Rol { get; set; }
}