namespace ProjectAccessibility.Models;

public class HeeftBeperking
{
    public int Bcode { get; set; }
    public int Ecode { get; set; }
    public Beperking Beperking { get; set; }
    public Ervaringdeskundige Ervaringdeskundige { get; set; }
}