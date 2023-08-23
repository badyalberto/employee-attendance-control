using MediatR;
using ms.employees.application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Queries
{
    public record GetAllEmployeeQuery() : IRequest<IEnumerable<EmployeeResponse>>;

}
