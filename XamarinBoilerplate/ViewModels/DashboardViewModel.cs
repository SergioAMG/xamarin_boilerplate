using DataManagers.Interfaces;

namespace XamarinBoilerplate.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
            Title = Localization.AppResources.Home;
        }
    }
}
