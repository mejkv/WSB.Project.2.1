using AutoMapper;
using MagazynEdu.ApplicationsServices.API.Domain;
using MagazynEdu.DataAccess.CQRS;
using MagazynEdu.DataAccess.CQRS.Queries;
using MediatR;

namespace MagazynEdu.ApplicationsServices.API.Handlers
{
    public class GetDevicesHandler : IRequestHandler<GetDevicesRequest, GetDevicesResponse>
    {
        
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetDevicesHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetDevicesResponse> Handle(GetDevicesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetDevicesQuery()
            {
                Title = request.Title,
            };
            var devices = await this.queryExecutor.Execute(query);
            var mappedDevice = mapper.Map<List<Domain.Models.Device>>(devices);
            var response = new GetDevicesResponse()
            {
                Data = mappedDevice
            };
            return response;
        }
    }
}
