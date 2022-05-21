using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.DTOs
{
    public class UpdateInventoryDTO : AddInventoryDTO
    {
        [Key]
        public int Id { get; set; }
    }
}
