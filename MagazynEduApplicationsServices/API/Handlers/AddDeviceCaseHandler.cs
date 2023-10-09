using AutoMapper;
using MagazynEdu.ApplicationsServices.API.Domain;
using MagazynEdu.DataAccess.CQRS;
using MagazynEdu.DataAccess.CQRS.Commands;
using MagazynEdu.DataAccess.Entities;
using MediatR;

namespace MagazynEdu.ApplicationsServices.API.Handlers
{
    public class AddDeviceCaseHandler : IRequestHandler<AddDeviceCaseRequest, AddDeviceCaseResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IMapper mapper;

        public AddDeviceCaseHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }

        public async Task<AddDeviceCaseResponse> Handle(AddDeviceCaseRequest request, CancellationToken cancellationToken)
        {
            var deviceCase = this.mapper.Map<DeviceCase>(request);
            var command = new AddDeviceCaseCommand() { Parameter = deviceCase };
            var deviceCaseFromDb = await this.commandExecutor.Execute(command);
            return new AddDeviceCaseResponse()
            {
                Data = this.mapper.Map<Domain.Models.DeviceCase>(deviceCaseFromDb),
            };
        }
    }
}
