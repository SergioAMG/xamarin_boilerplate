using DataManagers.Entities;
using DataManagers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.DataObjects
{
    public class DONews : INews
    {
        public async Task<List<News>> GetNews()
        {       
            List<News> ListItems = new List<News>();
            SQLite.SQLiteAsyncConnection connection = DataManager.Instance.SqliteDatabase;
            List<News> news = await connection.Table<News>().ToListAsync();
            
            foreach (News item in news)
            {
                ListItems.Add(item);
            }

            return await Task.FromResult<List<News>>(ListItems);
        }
    }
}
