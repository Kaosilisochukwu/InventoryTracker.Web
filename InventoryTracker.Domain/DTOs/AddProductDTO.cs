using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Domain.DTOs
{
    public class AddProductDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field, Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field, Description is required")]
        public string Description { get; set; }

        [Range(1, 1000000000, ErrorMessage = "The input for UnitPrice is out of range")]
        [Required(ErrorMessage = "The Unit price of the product added is required")]
        public decimal UnitPrice { get; set; }
    }
}
