using AirShop.DataAccess.Data.Models;
using AirShop.WebApiPostgre.Data.Entities;
using AutoMapper;

namespace AirShop.DataAccess.Data.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.Discount, y => y.MapFrom(z => z.Discount))
                .ForMember(x => x.Vat, y => y.MapFrom(z => z.Vat))
                .ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.ProductType, y => y.MapFrom(z => z.ProductType))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Image));
        }
    }
}