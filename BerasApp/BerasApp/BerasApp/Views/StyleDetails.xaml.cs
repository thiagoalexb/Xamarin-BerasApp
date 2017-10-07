using BerasApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BerasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StyleDetails : ContentPage
    {
        public StyleDetails(BreweryDB.Models.Style obj)
        {
            InitializeComponent();
            BindingContext = new StyleDetailsViewModel(obj);
        }
    }
}
