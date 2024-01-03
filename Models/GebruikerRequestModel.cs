namespace ProjectAccessibility.Models;

public class GebruikerRequestModel
{
    public string Email { get; set; }
    public string Wachtwoord { get; set; }
    public GebruikerType UserType { get; set; }
}