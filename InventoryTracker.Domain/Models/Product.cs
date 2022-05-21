using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field, Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "The field, Description is required")]
        public string Description { get; set; }

        [Range(1, 1000000000, ErrorMessage = "The input for UnitPrice is out of range")]
        public decimal UnitPrice { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
