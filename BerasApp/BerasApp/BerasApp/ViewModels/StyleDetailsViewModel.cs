namespace BerasApp.ViewModels
{
    public class StyleDetailsViewModel : BaseViewModel
    {
        #region Propertys
        BreweryDB.Models.Style style;
        public BreweryDB.Models.Style Style
        {
            get => style;
            set => SetProperty(ref style, value);
        }
        #endregion

        public StyleDetailsViewModel(BreweryDB.Models.Style obj) : base($"{obj.ShortName} - Details")
        {
            Style = obj;
        }
    }
}
