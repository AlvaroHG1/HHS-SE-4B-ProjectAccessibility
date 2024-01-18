using System.Runtime.InteropServices.JavaScript;
using NuGet.Packaging.Signing;

namespace ProjectAccessibility.Models;

public class OnderzoekRequestModel
{
    public String Titel { get; set; }
    public String Beschrijving { get; set; }
    public String Locatie { get; set; }
    public DateOnly Startdatum { get; set; }
    public DateOnly Einddatum { get; set; }
    public String GezochteBeperking { get; set; }
    public int GezochtePostcode { get; set; }
    public int MinLeeftijd { get; set; }
    public int MaxLeeftijd { get; set; }
}