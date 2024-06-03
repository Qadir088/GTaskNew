namespace BigOnApp.Helpers.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendMailAsync(string to, string subject, string body);
}
