using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAccessibility.Models;
[Table("Chat")]
public class Chat
{
    public int Ccode { get; set; }
    public int SenderGCode { get; set; }
    public int RecieverGCode { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
}