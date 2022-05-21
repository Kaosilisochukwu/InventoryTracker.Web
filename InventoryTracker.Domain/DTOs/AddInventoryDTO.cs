using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.DTOs
{
    public class AddInventoryDTO
    {
        [Required(ErrorMessage = "The field LocationId is required")]
        public int WareHouseId { get; set; }

        [Required(ErrorMessage = "The field LocationId is required")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The input for Stock Count is out of range")]
        public int StockCount { get; set; }
    }
}
