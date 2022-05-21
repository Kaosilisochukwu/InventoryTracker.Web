using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Data.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WareHouseDTO>> GetAsync();
        Task<WareHouseDTO> GetAsync(int id);
        Task<int> AddAsync(WareHouseDTO Warehouse);
        Task<int> UpdateAsync(WareHouseDTO Warehouse);
        Task DeleteAsync(int id);
    }
}
