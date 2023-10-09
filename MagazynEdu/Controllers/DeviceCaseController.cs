using AutoMapper;
using MagazynEdu.ApplicationsServices.API.Domain;
using MagazynEdu.DataAccess;
using MagazynEdu.DataAccess.CQRS;
using MagazynEdu.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MagazynEdu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceCaseController : ControllerBase
    {
        private readonly IMediator mediator;

        public DeviceCaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> GetAllDevices([FromBody] AddDeviceCaseRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}