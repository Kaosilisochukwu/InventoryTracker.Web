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
    public class WarehouseService : IWarehouseService
    {
        private readonly InventoryDbContext context;
        private readonly IMapper mapper;
        public WarehouseService(InventoryDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(WareHouseDTO model)
        {
            var existingWarehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower() && !x.IsDeleted);
            if (existingWarehouse != null) throw new Exception($"Warehouse with name {model.Name} already exist");
            var warehouse = mapper.Map<WareHouse>(model);
            warehouse.DateAdded = DateTime.Now;
            warehouse.IsDeleted = false;
            context.Add(warehouse);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (warehouse == null) throw new Exception("Warehouse does not exist");
            warehouse.IsDeleted = true; 
            var inventoriesWithWarehouse = await context.Inventories.Where(x => x.WareHouseId == warehouse.Id).ToListAsync();
            if (inventoriesWithWarehouse?.Any() ?? false)
            {
                inventoriesWithWarehouse.ForEach(x => x.IsDeleted = true);
                context.UpdateRange(inventoriesWithWarehouse);
            }
            context.Update(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WareHouseDTO>> GetAsync()
        {
            var warehouse = await context.WareHouses.Where(x => !x.IsDeleted).ToListAsync();
            return mapper.Map<IEnumerable<WareHouseDTO>>(warehouse);
        }

        public async Task<WareHouseDTO> GetAsync(int id)
        {
            var warehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Id == id);
            if (warehouse == null) throw new Exception("WareHouse does not exist");
            return mapper.Map<WareHouseDTO>(warehouse);
        }

        public async Task<int> UpdateAsync(WareHouseDTO model)
        {
            var warehouse = await context.WareHouses.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
            if (warehouse == null) throw new Exception("Warehouse Does not exist");
            var warehouseToUpdate = mapper.Map(model, warehouse);
            warehouseToUpdate.DateModified = DateTime.Now;
            context.WareHouses.Update(warehouseToUpdate);
            return await context.SaveChangesAsync();
        }
    }
}
