using BerasApp.Helpers;
using BerasApp.Interfaces;
using BerasApp.Services;
using BreweryDB;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BerasApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly BreweryDbClient client = new BreweryDbClient("f2657d6ca1da84e1b58408b5e9f547e2");
        string title;
        public AzureService azureService;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public readonly INavigationService _navigationService;

        protected void Notify([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string prop = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            Notify(prop);
            return true;
        }

        public BaseViewModel(string title) {
            Title = title;
            _navigationService = DependencyService.Get<INavigationService>();
            azureService = DependencyService.Get<AzureService>();
        }
            

        public void Quit()
        {
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;
            _navigationService.NavigationToLogin();
            azureService.LogoutAsync();
        }
    }
}
