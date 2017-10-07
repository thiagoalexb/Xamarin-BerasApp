using BerasApp.Helpers;
using BerasApp.Interfaces;
using BerasApp.Services;
using BerasApp.Views;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BerasApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        ICommand loginCommand;

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));
        

        public LoginViewModel() : base("Login")
        {
            
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            else
                _navigationService.NavigationToStyles();
        }

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLogged)
                return Task.FromResult(true);

            return azureService.LoginAsync();
        }
    }
}
