using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms.attendances.application.Command;
using ms.attendances.application.Queries;
using ms.attendances.application.Request;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ms.attendances.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AttendancesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AttendancesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetAllAttendances(string userName)
            => Ok(await _mediator.Send(new GetAllAttendancesQuery(userName)));

        public async Task<IActionResult> CreateAttendance([FromBody] CreateAttendanceRequest createAttendanceRequest)
        {
            var userName = User.Claims.FirstOrDefault(
                claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _mediator.Send(new CreateAttendanceCommand(userName, createAttendanceRequest)));
        }

    }
}
