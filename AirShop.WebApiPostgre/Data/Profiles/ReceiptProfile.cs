using AirShop.WebApiPostgre.Data.Entities;
using AirShop.WebApiPostgre.Data.Models;
using AutoMapper;

namespace AirShop.WebApiPostgre.Data.Profiles
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptDao, Receipt>()
                .ForMember(dest => dest.ReceiptId, opt => opt.MapFrom(src => src.ReceiptId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.IsSimplifiedInvoice, opt => opt.MapFrom(src => src.IsSimplifiedInvoice));
        }
    }
}
