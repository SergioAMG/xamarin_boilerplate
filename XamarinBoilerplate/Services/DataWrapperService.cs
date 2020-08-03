using DataManagers.Interfaces;

namespace XamarinBoilerplate.Services
{
    public class DataWrapperService : IDataService
    {
        public INews News
        {
            get { return DataManagers.DataManager.Instance.News; }
        }

        public IFlights Flights
        {
            get { return DataManagers.DataManager.Instance.Flights; }
        }

        public IBrands Brands
        {
            get { return DataManagers.DataManager.Instance.Brands; }
        }
    }
}
