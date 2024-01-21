namespace ProjectAccessibility.Models.ReturnModels;

public class ChatReturnModel
{
    public int SenderGCode { get; set; }
    public int RecieverGCode { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
    public string SenderName { get; set; }
}