namespace ProjectAccessibility.Models;

public class HeeftVoogd
{
    public int Vcode { get; set; }
    public int Ecode { get; set; }

    // Navigation property to represent the relationship with Voogd
    public Voogd Voogd { get; set; }

    // Navigation property to represent the relationship with Ervaringdeskundige
    public Ervaringdeskundige Ervaringdeskundige { get; set; }
}