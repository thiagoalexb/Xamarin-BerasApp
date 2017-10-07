namespace BerasApp.ViewModels
{
    public class BeerDetailsViewModel : BaseViewModel
    {
        #region Propertys
        BreweryDB.Models.Beer beer;
        public BreweryDB.Models.Beer Beer
        {
            get => beer;
            set => SetProperty(ref beer, value);
        }

        string brewery;
        public string Brewery
        {
            get => brewery;
            set => SetProperty(ref brewery, value);
        }
        #endregion


        public BeerDetailsViewModel(BreweryDB.Models.Beer obj) : base($"{obj.NameDisplay} - Details")
        {
            Beer = obj;
            if(Beer.Labels == null)
            {
                Beer.Labels = new BreweryDB.Models.Labels();
                Beer.Labels.Large = "beer.png";
            }
            if (Beer.Glass == null)
            {
                Beer.Glass = new BreweryDB.Models.Glass();
                Beer.Glass.Name = "Any Glass";
            }
            if (string.IsNullOrEmpty(Beer.IsOrganic))
            {
                Beer.IsOrganic = "No";
            }
            else
            {
                if (Beer.IsOrganic.ToLower().Contains("n"))
                {
                    Beer.IsOrganic = "No";
                }
                else
                {
                    Beer.IsOrganic = "Yes";
                }
            }
            if (string.IsNullOrEmpty(Beer.ServingTemperature))
            {
                Beer.ServingTemperature = "Any Temperature";
            }
            if (string.IsNullOrEmpty(Beer.Description))
            {
                Beer.Description = "No Description";
            }
            if (string.IsNullOrEmpty(Beer.Brewery))
            {
                Brewery = "Many Brewery";
            }
            else
            {
                Brewery = Beer.Brewery;
            }
        }
    }
}
