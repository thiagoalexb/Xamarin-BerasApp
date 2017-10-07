using BerasApp.Interfaces;
using BerasApp.Views;
using BreweryDB.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BerasApp.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigationToBeers(BreweryDB.Models.Style obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Beers(obj));
        }

        public async Task NavigationToStyleDetails(BreweryDB.Models.Style obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new StyleDetails(obj));
        }

        public async Task NavigationToBeerDetails(Beer obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BeerDetails(obj));
        }

        public void NavigationToLogin()
        {
            App.Current.MainPage = new Login();
        }

        public void NavigationToStyles()
        {
            App.Current.MainPage = new NavigationPage(new Styles());
        }
    }
}
