using BerasApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BerasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Styles : ContentPage
    {
        public Styles()
        {
            InitializeComponent();
            BindingContext = new StylesViewModel();
        }

        private void ListGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
