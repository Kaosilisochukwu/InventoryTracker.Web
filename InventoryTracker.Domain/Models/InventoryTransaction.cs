﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Models
{
    public enum ActionType
    {
        None = 1,
        Restock,
        Sell,
        AddProduct,
        Delete,
        Modify
    }
    public class InventoryTransaction
    {
        [Key]
        public int Id { get; set; }
        public ActionType Action { get; set; }
        public string Comments { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "The input for quantity is out of range")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The field WarehouseId is Requierd")]
        public int WareHouseId { get; set; }

        [Required(ErrorMessage = "The field ProductId is Requierd")]
        public int ProductId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public WareHouse WareHouse { get; set; }
        public Product Product { get; set; }
    }
}