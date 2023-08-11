using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.application.Commands
{
    public record CreateUserAccountCommand(string username,string password, string role) : IRequest<string>; 
}
