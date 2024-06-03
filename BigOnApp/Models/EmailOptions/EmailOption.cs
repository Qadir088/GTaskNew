namespace BigOnApp.Models.EmailOptions;

public class EmailOption
{
    public string SmtpServer { get; set; }
    public short SmtpPort { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string Password { get; set; }
}
