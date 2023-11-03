using AutoMapper;
using MagazynEdu.WebApiPostgre.Entities;
using MagazynEdu.WebApiPostgre.Models;

namespace MagazynEdu.ApplicationsServices.Mappings
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            this.CreateMap<ProductDao, Product>()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.Discount, y => y.MapFrom(z => z.Discount))
                .ForMember(x => x.Vat, y => y.MapFrom(z => z.Vat))
                .ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}
