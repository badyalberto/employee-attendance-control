using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms.users.application.Commands;
using ms.users.application.Queries;
using ms.users.application.Requests;
using System.Threading.Tasks;

namespace ms.users.api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllUsers() => Ok(await _mediator.Send(new GetAllUsersQuery()));

        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest account) =>
            Ok(await _mediator.Send(new CreateUserAccountCommand(account.UserName,account.Password,account.Role)));


    }
}
