using MediatR;

namespace MagazynEdu.ApplicationsServices.API.Domain
{
    public class AddDeviceCaseRequest : IRequest<AddDeviceCaseResponse>
    {
        public int Number { get; set; }
    }
}
