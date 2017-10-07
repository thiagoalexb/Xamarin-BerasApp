using System.Threading.Tasks;

namespace BerasApp.Interfaces
{
    public interface INavigationService
    {
        void NavigationToLogin();
        void NavigationToStyles();
        Task NavigationToBeers(BreweryDB.Models.Style obj);
        Task NavigationToStyleDetails(BreweryDB.Models.Style obj);
        Task NavigationToBeerDetails(BreweryDB.Models.Beer obj);
    }
}
