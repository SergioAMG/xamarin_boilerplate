using DataManagers.Entities;
using DataManagers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.DataObjects
{
    public class DOBrands : IBrands
    {
        public async Task<List<Brands>> GetBrands()
        {
            List<Brands> ListItems = new List<Brands>();

            SQLite.SQLiteAsyncConnection connection = DataManager.Instance.SqliteDatabase;
            List<Brands> brands = await connection.Table<Brands>().ToListAsync();

            foreach (Brands item in brands)
            {
                ListItems.Add(item);
            }

            return await Task.FromResult<List<Brands>>(ListItems);
        }
    }
}
