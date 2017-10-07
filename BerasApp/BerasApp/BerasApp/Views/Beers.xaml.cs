using BerasApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BerasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Beers : ContentPage
    {
        public Beers(BreweryDB.Models.Style obj)
        {
            InitializeComponent();
            BindingContext = new BeersViewModel(obj);
        }

        private void ListGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
