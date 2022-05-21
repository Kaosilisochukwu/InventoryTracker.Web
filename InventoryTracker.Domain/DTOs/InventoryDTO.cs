using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.DTOs
{
    public class InventoryDTO : UpdateInventoryDTO
    {
        public DateTime DateAdded { get; set; }
        public WareHouseDTO WareHouse { get; set; }
        public ProductDTO Product { get; set; }
    }
}
