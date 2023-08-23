using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Commands
{
    public record UpdateAttendanceStateCommand(string UserName,bool Attendance, string Notes) : IRequest<string>;
}
