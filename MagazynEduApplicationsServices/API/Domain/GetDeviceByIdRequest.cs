using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.ApplicationsServices.API.Domain
{
    public class GetDeviceByIdRequest : IRequest<GetDeviceByIdResponse>
    {
        public int DeviceId { get; set; }
    }
}
