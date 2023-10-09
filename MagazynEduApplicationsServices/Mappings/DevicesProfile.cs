using AutoMapper;
using MagazynEdu.ApplicationsServices.API.Domain.Models;

namespace MagazynEdu.ApplicationsServices.Mappings
{
    public class DevicesProfile : Profile
    {
        public DevicesProfile()
        {
            this.CreateMap<MagazynEdu.DataAccess.Entities.Device, Device>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Title, y => y.MapFrom(z => z.Title));
        }
    }
}
