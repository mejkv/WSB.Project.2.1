using AirShop.WebApiPostgre.Data.Entities;
using AirShop.WebApiPostgre.Data.Models;
using AutoMapper;

namespace AirShop.WebApiPostgre.Data.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductDao, Product>()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.Discount, y => y.MapFrom(z => z.Discount))
                .ForMember(x => x.Vat, y => y.MapFrom(z => z.Vat))
                .ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}
