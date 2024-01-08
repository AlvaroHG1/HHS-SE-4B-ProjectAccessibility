using System.Runtime.InteropServices.JavaScript;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;

namespace ProjectAccessibility.Models;

public class Onderzoek
{
    public int Ocode { get; set; }
    public int Otcode { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    public DateOnly Startdatum { get; set; }
    public DateOnly Einddatum { get; set; }

}