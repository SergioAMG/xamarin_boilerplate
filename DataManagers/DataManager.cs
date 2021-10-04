using DataManagers.DataObjects;
using SQLite;

namespace DataManagers
{
    public class DataManager
    {
        private DONews _doNews;
        private DOFlights _doFlights;
        private DOBrands _doBrands;
        private static DataManager _instance;
        private SQLiteAsyncConnection _sqlitedatabase;

        public DONews News
        {
            get { return _doNews ?? (_doNews = new DONews()); }
        }

        public DOFlights Flights
        {
            get { return _doFlights ?? (_doFlights = new DOFlights()); }
        }

        public DOBrands Brands
        {
            get { return _doBrands ?? (_doBrands = new DOBrands()); }
        }

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

        public SQLiteAsyncConnection SqliteDatabase
        {
            get
            {
                return _sqlitedatabase;
            }
        }

        public void ConnectToLocalDatabase(string dbPath)
        {
            _sqlitedatabase = new SQLiteAsyncConnection(dbPath);
        }
    }
}
