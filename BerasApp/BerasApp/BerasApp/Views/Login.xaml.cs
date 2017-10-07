using BerasApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BerasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
