using CookBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllUnits();
    }
}
