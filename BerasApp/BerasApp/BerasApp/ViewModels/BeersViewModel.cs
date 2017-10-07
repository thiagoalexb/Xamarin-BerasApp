using BerasApp.Interfaces;
using BreweryDB.Models.RequestParameters;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BerasApp.ViewModels
{
    public class BeersViewModel : BaseViewModel
    {
        #region Propertys
        ObservableCollection<BreweryDB.Models.Beer> listBeers = new ObservableCollection<BreweryDB.Models.Beer>();
        public ObservableCollection<BreweryDB.Models.Beer> ListBeers
        {
            get => listBeers;
            set => SetProperty(ref listBeers, value);
        }

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        int Page;
        string StyleId;
        #endregion

        #region Commands
        public Command QuitCommand { get; }
        public Command GetCommand { get; }
        public Command<BreweryDB.Models.Beer> BeerDetailCommand { get; }
        public Command GetMoreCommand { get; }
        #endregion
        
        public BeersViewModel(BreweryDB.Models.Style obj) : base($"{obj.ShortName} - Beers")
        {
            QuitCommand = new Command(Quit);
            GetCommand = new Command(Get);
            BeerDetailCommand = new Command<BreweryDB.Models.Beer>(BeerDetail);
            GetMoreCommand = new Command(GetMore);

            ListBeers = new ObservableCollection<BreweryDB.Models.Beer>();
            
            Page = 1;
            StyleId = obj.Id;

            Get();
        }

        async void BeerDetail(BreweryDB.Models.Beer obj) =>
            await _navigationService.NavigationToBeerDetails(obj);
        
        async void Get()
        {
            IsBusy = true;
            var parameters = new BreweryDB.Helpers.NameValueCollection { { BeerRequestParameters.StyleId, StyleId }, { BeerRequestParameters.Page, Page.ToString() }, {BeerRequestParameters.WithBreweries, "Y" } };
            var response = await client.Beers.Get(parameters);
            ListBeers = new ObservableCollection<BreweryDB.Models.Beer>(response.Data);
            IsBusy = false;
        }

        async void GetMore()
        {
            IsBusy = true;
            Page++;
            var parameters = new BreweryDB.Helpers.NameValueCollection { { BeerRequestParameters.StyleId, StyleId }, { BeerRequestParameters.Page, Page.ToString() } };
            var response = await client.Beers.Get(parameters);
            foreach (var item in response.Data)
            {
                ListBeers.Add(item);
            }
            IsBusy = false;
        }
    }
}
