using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms.users.application.Queries;
using ms.users.application.Requests;
using System.Threading.Tasks;

namespace ms.users.api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DoLogin([FromBody] LoginCredentialsRequest loginCredentials) =>
            Ok(await _mediator.Send(new GetUserTokenQuery(loginCredentials.UserName,loginCredentials.Password)));

    }
}
