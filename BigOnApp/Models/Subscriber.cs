namespace BigOnApp.Models;

public class Subscriber
{
    public string EmailAdreess { get; set; }
    public bool IsAprovved { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? AprovvedAt { get; set;}
}
