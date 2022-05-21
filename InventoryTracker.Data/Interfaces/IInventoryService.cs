using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Data.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetAsync();
        Task<InventoryDTO> GetAsync(int id);
        Task<int> AddAsync(InventoryDTO inventory);
        Task<int> UpdateAsync(InventoryDTO inventory);
        Task DeleteAsync(int id);
        Task<int> ModifyInventoryStock(ActionType action, int id, int quantity);
        Task<IEnumerable<InventoryDTO>> GetByProduct(int productId);
        Task<IEnumerable<InventoryDTO>> GetByWarehouse(int warehouseId);
    }
}
