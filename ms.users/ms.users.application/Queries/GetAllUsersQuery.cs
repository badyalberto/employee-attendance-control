using MediatR;
using ms.users.application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.application.Queries
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserResponse>>;
}
