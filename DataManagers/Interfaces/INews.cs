using DataManagers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.Interfaces
{
    public interface INews
    {
        Task<List<News>> GetNews();
    }
}
