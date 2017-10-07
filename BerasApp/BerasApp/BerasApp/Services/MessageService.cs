using BerasApp.Interfaces;
using System.Threading.Tasks;

namespace BerasApp.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowMessageAsync(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        }

        public async Task<bool> ShowConfirmationMessageAsync(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, "Sim", "Não");
        }
    }
}
