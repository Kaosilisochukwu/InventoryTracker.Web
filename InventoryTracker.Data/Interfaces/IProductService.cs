using InventoryTracker.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Data.Interfaces
{
    public  interface IProductService
    {

        Task<IEnumerable<ProductDTO>> GetAsync();
        Task<ProductDTO> GetAsync(int id);
        Task<int> AddAsync(ProductDTO Warehouse);
        Task<int> UpdateAsync(ProductDTO Warehouse);
        Task DeleteAsync(int id);
    }
}
