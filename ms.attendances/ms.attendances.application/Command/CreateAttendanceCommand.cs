using MediatR;
using ms.attendances.application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.application.Command
{
    public record CreateAttendanceCommand(string UserName,CreateAttendanceRequest AttendaceRequest) : IRequest<bool>;

}
