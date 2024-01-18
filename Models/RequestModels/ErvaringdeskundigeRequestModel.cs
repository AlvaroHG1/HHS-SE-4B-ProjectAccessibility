namespace ProjectAccessibility.Models;

public class ErvaringdeskundigeRequestModel
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string Telefoonnummer { get; set; }
    public string Plaats { get; set; }
    public string Postcode { get; set; }
    public string Straatnaam { get; set; }
    public string Huisnummer { get; set; }
    public string Contactvoorkeur { get; set; }
    public bool Commercieel { get; set; }
    public string Email { get; set; }
    public string Wachtwoord { get; set; }
    public DateTime Geboortedatum { get; set; }
}