namespace ProjectAccessibility.Models;

public class ChatRequestModel
{
    public int SenderGCode { get; set; }
    public int RecieverGCode { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; }
}