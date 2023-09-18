using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Commands
{
    public record CreateEmployeeCommand(string UserName,string FirstName, string LastName,string password, string Role) : IRequest<string>;
}
