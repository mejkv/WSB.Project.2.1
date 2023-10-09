using AutoMapper;
using MagazynEdu.ApplicationsServices.API.Domain;
using MagazynEdu.DataAccess.CQRS.Commands;
using MagazynEdu.DataAccess.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazynEdu.ApplicationsServices.API.Domain.Models;
using MagazynEdu.DataAccess.CQRS.Queries;

namespace MagazynEdu.ApplicationsServices.API.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetDeviceByIdRequest, GetDeviceByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetBookByIdHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetDeviceByIdResponse> Handle(GetDeviceByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetDeviceQuery()
            {
                Id = request.DeviceId
            };
            var device = await this.queryExecutor.Execute(query);
            var mappedDevice = this.mapper.Map<Device>(device);
            return new GetDeviceByIdResponse()
            {
                Data = mappedDevice,
            };
        }
    }
}