using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, string subject, string message);
    }
}
