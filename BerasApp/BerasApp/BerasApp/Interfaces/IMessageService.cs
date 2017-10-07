using System.Threading.Tasks;

namespace BerasApp.Interfaces
{
    public interface IMessageService
    {
        Task ShowMessageAsync(string title, string message);
        Task<bool> ShowConfirmationMessageAsync(string title, string message);
    }
}
