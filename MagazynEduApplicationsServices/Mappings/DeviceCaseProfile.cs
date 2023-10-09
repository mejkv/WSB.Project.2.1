using AutoMapper;
using MagazynEdu.DataAccess.Entities;
using MagazynEdu.ApplicationsServices.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.ApplicationsServices.Mappings
{
    public class DeviceCaseProfile : Profile
    {
        public DeviceCaseProfile()
        {
            this.CreateMap<AddDeviceCaseRequest, DataAccess.Entities.DeviceCase>()
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number));

            this.CreateMap<DeviceCase, MagazynEdu.ApplicationsServices.API.Domain.Models.DeviceCase>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number));
        }
    }
}
