using DataManagers.Entities;
using DataManagers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.DataObjects
{
    public class DOFlights : IFlights
    {
        public async Task<List<Flights>> GetFlights()
        {
            List<Flights> ListItems = new List<Flights>();
            SQLite.SQLiteAsyncConnection connection = DataManager.Instance.SqliteDatabase;
            List<Flights> flights = await connection.Table<Flights>().ToListAsync();

            foreach (Flights item in flights)
            {
                ListItems.Add(item);
            }

            return await Task.FromResult<List<Flights>>(ListItems);
        }
    }
}
