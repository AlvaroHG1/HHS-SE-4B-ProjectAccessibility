using Npgsql.Internal.TypeHandlers.DateTimeHandlers;

namespace ProjectAccessibility.Models;

public class Onderzoek
{
    public int Ocode { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    public TimestampTzHandler Startdatum { get; set; }
    public TimestampTzHandler Einddatum { get; set; }
}