using DataManagers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.Interfaces
{
    public interface IFlights
    {
        Task<List<Flight>> GetFlights();
    }
}
