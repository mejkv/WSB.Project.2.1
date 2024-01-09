using AirShop.DataAccess.Data.Entities;
using AirShop.DataAccess.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Profiles
{
    public class DocumentPositionProfile : Profile
    {
        public DocumentPositionProfile()
        {
            CreateMap<DocumentPositionDto, DocumentPosition>();
        }
    }
}
