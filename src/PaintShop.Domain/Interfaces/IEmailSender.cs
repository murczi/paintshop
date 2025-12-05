namespace PaintShop.Domain.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string to);
}
