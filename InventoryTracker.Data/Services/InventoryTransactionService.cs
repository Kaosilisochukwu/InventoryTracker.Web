using AutoMapper;
using InventoryTracker.Data.Interfaces;
using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Data.Services
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly InventoryDbContext context;
        private readonly IMapper mapper;

        public InventoryTransactionService(InventoryDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(AddInventoryTransactionDTO model)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.ProductId && !x.IsDeleted);
            if (product == null) throw new Exception("Product does not exist");
            var warehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Id == model.WareHouseId && !x.IsDeleted);
            if (product == null) throw new Exception("Warehouse does not exist");
            var inventoryTransactions = mapper.Map<InventoryTransaction>(model);
            inventoryTransactions.DateAdded = DateTime.Now;
            inventoryTransactions.IsDeleted = false;
            context.Add(inventoryTransactions);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inventoryTransactions = await context.InventoryTransactions.FirstOrDefaultAsync(x => x.Id == id);
            if (inventoryTransactions == null) throw new Exception("Inventory Transactions does not exist");
            inventoryTransactions.IsDeleted = true;
            context.Update(inventoryTransactions);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetAsync()
        {
            var inventoryTransactions = await context.InventoryTransactions.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted).ToListAsync();
            return mapper.Map<IEnumerable<InventoryTransactionDTO>>(inventoryTransactions);
        }

        public async Task<InventoryTransactionDTO> GetAsync(int id)
        {
            var inventoryTransactions = await context.InventoryTransactions.Include(x => x.Product).Include(x => x.WareHouse).FirstOrDefaultAsync(x => x.Id == id);
            if (inventoryTransactions == null) throw new Exception("Inventory Transactions does not exist");
            return mapper.Map<InventoryTransactionDTO>(inventoryTransactions);
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetByProduct(int productId)
        {
            var inventoryTransactionDTO = await context.InventoryTransactions.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted && x.ProductId == productId).ToListAsync();
            return mapper.Map<IEnumerable<InventoryTransactionDTO>>(inventoryTransactionDTO);
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetByWarehouse(int warehouseId)
        {
            var inventoryTransactionDTO = await context.InventoryTransactions.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted && x.WareHouseId == warehouseId).ToListAsync();
            return mapper.Map<IEnumerable<InventoryTransactionDTO>>(inventoryTransactionDTO);
        }

        public async Task<int> UpdateAsync(UpdateInventoryTransactionDTO model)
        {
            var inventoryTransaction = await context.InventoryTransactions.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
            if (inventoryTransaction == null) throw new Exception("inventory Does not exist");
            var inventoryTransactionToUpdate = mapper.Map(model, inventoryTransaction);
            inventoryTransactionToUpdate.DateModified = DateTime.Now;
            context.InventoryTransactions.Update(inventoryTransactionToUpdate);
            return await context.SaveChangesAsync();
        }
    }
}
