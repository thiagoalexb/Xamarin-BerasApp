using BerasApp.Interfaces;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BerasApp.ViewModels
{
    public class StylesViewModel : BaseViewModel
    {
        #region Propertys
        private string url;
        ObservableCollection<BreweryDB.Models.Style> listStyles = new ObservableCollection<BreweryDB.Models.Style>();
        public ObservableCollection<BreweryDB.Models.Style> ListStyles
        {
            get => listStyles;
            set => SetProperty(ref listStyles, value);
        }

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        #endregion
        
        #region Commands
        public Command QuitCommand { get; }
        public Command GetCommand { get; }
        public Command<BreweryDB.Models.Style> SeeBeersCommand { get; }
        public Command<BreweryDB.Models.Style> StyleDetailsCommand { get; }
        #endregion

        public StylesViewModel() : base("Beer's Styles")
        {
            QuitCommand = new Command(Quit);
            GetCommand = new Command(Get);
            SeeBeersCommand = new Command<BreweryDB.Models.Style>(SeeBeers);
            StyleDetailsCommand = new Command<BreweryDB.Models.Style>(StyleDetails);

            ListStyles = new ObservableCollection<BreweryDB.Models.Style>();

            Get();
        }

        async void SeeBeers(BreweryDB.Models.Style obj) =>
            await  _navigationService.NavigationToBeers(obj);

        async void StyleDetails(BreweryDB.Models.Style obj) =>
            await _navigationService.NavigationToStyleDetails(obj);

        async void Get()
        {
            IsBusy = true;
            var response = await client.Styles.GetAll();
            ListStyles = new ObservableCollection<BreweryDB.Models.Style>(response.Data);
            IsBusy = false;
        }
    }
}
