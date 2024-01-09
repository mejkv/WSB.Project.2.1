using AirShop.DataAccess.Data.Models;
using AirShop.WebApiPostgre.Data.Entities;
using AutoMapper;

namespace AirShop.DataAccess.Data.Profiles
{
    public class CodeProfile : Profile
    {
        public CodeProfile()
        {
            CreateMap<CodeDto, Code>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.Ean, y => y.MapFrom(z => z.Ean));
                //.ForMember(x => x.Product, y => y.MapFrom(z => z.Product));
        }
    }
}