using AutoMapper;
using InventoryTracker.Domain.DTOs;
using InventoryTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<WareHouse, WareHouseDTO>();
            CreateMap<WareHouseDTO, WareHouse>();
            CreateMap<UpdateWareHouseDTO, WareHouse>();
            CreateMap<AddInventoryDTO, Inventory>();
            CreateMap<UpdateInventoryDTO, Inventory>();
            CreateMap<AddInventoryDTO, AddInventoryTransactionDTO>();
            CreateMap<Inventory, InventoryDTO>()
                .ForMember(x => x.WareHouse, opt => opt.MapFrom(src => src.WareHouse))
                .ForMember(x => x.Product, opt => opt.MapFrom(src => src.Product));
            CreateMap<AddInventoryTransactionDTO, InventoryTransaction>();
            CreateMap<UpdateInventoryTransactionDTO, InventoryTransaction>();
            CreateMap<InventoryTransaction, InventoryTransactionDTO>()
                .ForMember(x => x.WareHouse, opt => opt.MapFrom(src => src.WareHouse))
                .ForMember(x => x.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}
