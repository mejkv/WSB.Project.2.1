using MediatR;

namespace MagazynEdu.ApplicationsServices.API.Domain
{
    public class GetDevicesRequest : IRequest<GetDevicesResponse>
    {
        public string Title { get; set; }
    }
}
