namespace ProjectAccessibility.Models;

public class HeeftType
{
    public int Otcode { get; set; }
    public int Ocode { get; set; }
    public Onderzoekstype Onderzoekstype { get; set; }
    public Onderzoek Onderzoek { get; set; }
}