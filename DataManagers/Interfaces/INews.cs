using DataManagers.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DataManagers.Interfaces
{
    public interface INews
    {
        Task<ObservableCollection<News>> GetNews();
    }
}
