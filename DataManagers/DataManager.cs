using DataManagers.DataObjects;

namespace DataManagers
{
    public class DataManager
    {
        private DONews _doNews;
        private DOFlights _doFlights;
        private static DataManager _instance;

        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataManager();
                }
                return _instance;
            }
        }

        public DONews News
        {
            get { return _doNews ?? (_doNews = new DONews()); }
        }

        public DOFlights Flights
        {
            get { return _doFlights ?? (_doFlights = new DOFlights()); }
        }
    }
}
