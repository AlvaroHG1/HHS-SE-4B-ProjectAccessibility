namespace ProjectAccessibility.Models;

public class HeeftOnderzoek
{
    public int Ocode { get; set; }
    public int Bcode { get; set; }
    public Onderzoek Onderzoek { get; set; }
    
    public Bedrijf Bedrijf { get; set; }
}