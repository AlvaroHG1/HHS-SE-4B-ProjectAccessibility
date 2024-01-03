namespace ProjectAccessibility.Models;

public class HeeftHulpmiddel
{
    public int Hcode { get; set; }
    public int Ecode { get; set; }
    public Hulpmiddel Hulpmiddel { get; set; }
    public Ervaringdeskundige Ervaringdeskundige { get; set; }
}