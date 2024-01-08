using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using NuGet.Packaging.Signing;

namespace ProjectAccessibility.Models;

public class OnderzoekRequestModel
{
    public String Titel { get; set; }
    public String Beschrijving { get; set; }
    public String Locatie { get; set; }
    public TimestampTzHandler Startdatum { get; set; }
    public TimestampTzHandler Einddatum { get; set; }
}