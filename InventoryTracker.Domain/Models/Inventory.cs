using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field LocationId is required")]
        public int WareHouseId { get; set; }

        [Required(ErrorMessage = "The field LocationId is required")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The input for Stock Count is out of range")]
        public int StockCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public WareHouse WareHouse { get; set; }
        public Product Product { get; set; }
    }
}
