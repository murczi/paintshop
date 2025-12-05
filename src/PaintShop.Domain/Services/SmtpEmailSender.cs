using PaintShop.Domain.Interfaces;

namespace PaintShop.Domain.Services;

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

public sealed class SmtpEmailSender(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendEmailAsync(string to)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.User, _settings.Password)
        };

        using var message = new MailMessage(_settings.From, to, "FestekBolt Hirlevel", "Ez egy hirlevel a festekbolttol")
        {
            IsBodyHtml = false
        };

        await client.SendMailAsync(message);
    }
}
