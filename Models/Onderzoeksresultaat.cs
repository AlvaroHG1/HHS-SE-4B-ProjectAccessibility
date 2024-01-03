namespace ProjectAccessibility.Models;

public class Onderzoeksresultaat
{
    public int Orcode { get; set; }
    public int Ocode { get; set; }
    public int Ecode { get; set; }
    public DateTime? Datum { get; set; }
    public string Antwoord { get; set; }
    public Onderzoek Onderzoek { get; set; }
    public Ervaringdeskundige Ervaringdeskundige { get; set; }
}