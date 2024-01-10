namespace ProjectAccessibility.Models.ReturnModels;

public class ErvaringdeskundigeReturnModel
{
    public Ervaringdeskundige Ervaringdeskundige { get; set; }
    public List<string> Beperkingen { get; set; }
    public List<string> Hulpmiddellen { get; set; }
    
    public List<string> Aandoeningen { get; set; }
    public List<string> VoorkeurTypes { get; set; }
    
}