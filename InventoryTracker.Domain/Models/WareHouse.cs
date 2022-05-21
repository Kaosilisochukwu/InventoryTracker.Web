using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Models
{
    public class WareHouse
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field for Location name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field Postal code is required")]
        [DataType(DataType.PostalCode, ErrorMessage = "The Field for Postal code is not in the correct format")]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field Address is required")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field Postal code is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Field for Email Address is not in the correct format")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field Postal code is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The Field for Phone Number is not in the correct format")]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
