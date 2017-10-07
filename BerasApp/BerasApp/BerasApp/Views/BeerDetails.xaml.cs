using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BerasApp.ViewModels;

namespace BerasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeerDetails : ContentPage
    {
        public BeerDetails(BreweryDB.Models.Beer obj)
        {
            InitializeComponent();
            BindingContext = new BeerDetailsViewModel(obj);
        }
    }
}
