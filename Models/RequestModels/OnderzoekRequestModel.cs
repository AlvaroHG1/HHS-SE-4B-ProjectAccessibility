using System.Runtime.InteropServices.JavaScript;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using NuGet.Packaging.Signing;

namespace ProjectAccessibility.Models;

public class OnderzoekRequestModel
{
    public int Otcode { get; set; }
    public String Titel { get; set; }
    public String Beschrijving { get; set; }
    public String Locatie { get; set; }
    public DateOnly Startdatum { get; set; }
    public DateOnly Einddatum { get; set; }
}