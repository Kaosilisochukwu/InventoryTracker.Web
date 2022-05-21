using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.DTOs
{
    public class ProductDTO : UpdateProductDTO
    {
        public DateTime DateAdded { get; set; }
    }
}
