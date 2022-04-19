using DataManagers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagers.Interfaces
{
    public interface IBrands
    {
        Task<List<Brands>> GetBrands();
    }
}
