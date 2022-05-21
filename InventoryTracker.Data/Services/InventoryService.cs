using AutoMapper;
using InventoryTracker.Data.Interfaces;
using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext context;
        private readonly IMapper mapper;
        private readonly IInventoryTransactionService transactionService;

        public InventoryService(InventoryDbContext context, IMapper mapper, IInventoryTransactionService transactionService)
        {
            this.context = context;
            this.mapper = mapper;
            this.transactionService = transactionService;
        }
        public async Task<int> AddAsync(InventoryDTO model)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.ProductId && !x.IsDeleted);
            if (product == null) throw new Exception("Product does not exist");
            var warehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Id == model.WareHouseId && !x.IsDeleted);
            if (warehouse == null) throw new Exception("Warehouse does not exist");
            var existingInventory = context.Inventories.FirstOrDefault(x => x.WareHouseId == model.WareHouseId && x.ProductId == model.ProductId);
            if(existingInventory != null)
            {
                if (existingInventory.IsDeleted)
                {
                    existingInventory.StockCount = model.StockCount;
                    existingInventory.IsDeleted = false;
                }
                else
                {
                    existingInventory.StockCount += model.StockCount;
                }

                existingInventory.DateModified = DateTime.Now;
                context.Update(existingInventory);
                return context.SaveChanges();
            }

            var inventory = mapper.Map<Inventory>(model);
            inventory.DateAdded = DateTime.Now;
            inventory.IsDeleted = false;
            if (inventory == null) throw new Exception("Inventory does not exist");
            var transaction = new AddInventoryTransactionDTO
            {
                Action = ActionType.AddProduct,
                Quantity = 0,
                WareHouseId = inventory.WareHouseId,
                ProductId = inventory.ProductId,
                Comments = $"{model.StockCount} of Product {product.Name} was Added to {warehouse.Name}"
            };
            await transactionService.AddAsync(transaction);
            context.Add(inventory);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).FirstOrDefaultAsync(x => x.Id == id);
            if (inventory == null) throw new Exception("Inventory does not exist");
            var transaction = new AddInventoryTransactionDTO
            {
                Action = ActionType.AddProduct,
                Quantity = 0,
                WareHouseId = inventory.WareHouseId,
                ProductId = inventory.ProductId,
                Comments = $"Product {inventory.Product.Name} was Deleted from {inventory.WareHouse.Name}"
            };
            await transactionService.AddAsync(transaction);
            inventory.IsDeleted = true;
            inventory.StockCount = 0;
            context.Update(inventory);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InventoryDTO>> GetAsync()
        {
            var inventories = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted).ToListAsync();
            return mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<InventoryDTO> GetAsync(int id)
        {
            var inventory = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).FirstOrDefaultAsync(x => x.Id == id);
            if (inventory == null) throw new Exception("Inventory does not exist");
            return mapper.Map<InventoryDTO>(inventory);
        }

        public async Task<IEnumerable<InventoryDTO>> GetByProduct(int productId)
        {
            var inventories = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted && x.ProductId == productId).ToListAsync();
            return mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<IEnumerable<InventoryDTO>> GetByWarehouse(int warehouseId)
        {
            var inventories = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).Where(x => !x.IsDeleted && x.WareHouseId == warehouseId).ToListAsync();
            return mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<int> ModifyInventoryStock(ActionType action, int id, int quantity)
        {
            var inventory = await context.Inventories.Include(x => x.Product).Include(x => x.WareHouse).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (inventory == null) throw new Exception("Inventory does not exist");
            var transaction = new AddInventoryTransactionDTO
            {
                Action = action,
                Quantity = quantity,
                WareHouseId = inventory.WareHouseId,
                ProductId = inventory.ProductId,
            };
            switch (action)
            {
                case ActionType.None:
                    return 0;
                case ActionType.Restock:
                    inventory.StockCount += quantity;
                    transaction.Comments = $"{quantity} of Product {inventory.Product.Name} was Restocked to {inventory.WareHouse.Name}";
                    break;
                case ActionType.Sell:
                    inventory.StockCount -= quantity;
                    transaction.Comments = $"{quantity} of Product {inventory.Product.Name} was removed from {inventory.WareHouse.Name}";
                    break;
                case ActionType.Delete:
                    inventory.StockCount = 0;
                    transaction.Comments = $"{quantity} of Product {inventory.Product.Name} was Restocked to {inventory.WareHouse.Name}";
                    break;
                default:
                    break;
            }
            await transactionService.AddAsync(transaction);
            inventory.DateModified = DateTime.Now;
            context.Inventories.Update(inventory);
            return context.SaveChanges();
        }

        public async Task<int> UpdateAsync(InventoryDTO model)
        {
            var inventory = await context.Inventories.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
            if (inventory == null) throw new Exception("inventory Does not exist");
            var inventoryToUpdate = mapper.Map(model, inventory);
            inventory.DateModified = DateTime.Now;
            var transaction = new AddInventoryTransactionDTO
            {
                Action = ActionType.Modify,
                Quantity = 0,
                WareHouseId = inventory.WareHouseId,
                ProductId = inventory.ProductId,
                Comments = $"Inventory for Product {inventory.Product.Name} was modified in {inventory.WareHouse.Name}"
            };
            await transactionService.AddAsync(transaction);
            context.Inventories.Update(inventoryToUpdate);
            return await context.SaveChangesAsync();
        }
    }
}
