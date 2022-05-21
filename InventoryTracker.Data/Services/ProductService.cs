using AutoMapper;
using InventoryTracker.Data.Interfaces;
using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data.Services
{
    public class ProductService : IProductService
    {

        private readonly InventoryDbContext context;
        private readonly IMapper mapper;
        public ProductService(InventoryDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ProductDTO model)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower() && !x.IsDeleted);
            if (existingProduct != null) throw new Exception($"Product with name {model.Name} already exist");
            var product = mapper.Map<Product>(model);
            product.DateAdded = DateTime.Now;
            product.IsDeleted = false;
            context.Add(product);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Product = await context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (Product == null) throw new Exception("Product does not exist");
            Product.IsDeleted = true;
            var inventoriesWithProducts = await context.Inventories.Where(x => x.ProductId == Product.Id).ToListAsync();
            if(inventoriesWithProducts?.Any() ?? false)
            {
                inventoriesWithProducts.ForEach(x => x.IsDeleted = true);
                context.UpdateRange(inventoriesWithProducts);
            }
            context.Update(Product);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAsync()
        {
            var inventoryTransactions = await context.Products.Where(x => !x.IsDeleted).ToListAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(inventoryTransactions);
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var inventoryTransactions = await context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (inventoryTransactions == null) throw new Exception("Product does not exist");
            return mapper.Map<ProductDTO>(inventoryTransactions);
        }

        public async Task<int> UpdateAsync(ProductDTO model)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
            if (product == null) throw new Exception("Product Does not exist");
            var productToUpdate = mapper.Map(model, product);
            productToUpdate.DateModified = DateTime.Now;
            context.Products.Update(productToUpdate);
            return await context.SaveChangesAsync();
        }
    }
}
