using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.application.Queries
{
    public record GetUserTokenQuery(string Username, string Password) : IRequest<string>;
}
