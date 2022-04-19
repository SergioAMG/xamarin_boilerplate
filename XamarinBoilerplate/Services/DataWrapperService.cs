using DataManagers;
using DataManagers.Interfaces;

namespace XamarinBoilerplate.Services
{
    public class DataWrapperService : IDataService
    {
        public INews News
        {
            get { return DataManager.Instance.News; }
        }

        public IFlights Flights
        {
            get { return DataManager.Instance.Flights; }
        }

        public IBrands Brands
        {
            get { return DataManager.Instance.Brands; }
        }
    }
}
