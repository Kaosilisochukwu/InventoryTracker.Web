using InventoryTracker.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Data.Interfaces
{
    public interface IInventoryTransactionService
    {
        Task<IEnumerable<InventoryTransactionDTO>> GetAsync();
        Task<InventoryTransactionDTO> GetAsync(int id);
        Task<int> AddAsync(AddInventoryTransactionDTO Warehouse);
        Task<int> UpdateAsync(UpdateInventoryTransactionDTO Warehouse);
        Task DeleteAsync(int id);
        Task<IEnumerable<InventoryTransactionDTO>> GetByProduct(int productId);
        Task<IEnumerable<InventoryTransactionDTO>> GetByWarehouse(int productId);
    }
}
