using System.Runtime.InteropServices.JavaScript;
namespace ProjectAccessibility.Models;

public class Onderzoek
{
    public int Ocode { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    public DateTime Startdatum { get; set; }
    public DateTime Einddatum { get; set; }
    public String GezochteBeperking { get; set; }
    public int GezochtePostcode { get; set; }
    public int MinLeeftijd { get; set; }
    public int MaxLeeftijd { get; set; }

}