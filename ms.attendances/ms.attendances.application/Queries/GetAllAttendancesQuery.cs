using MediatR;
using ms.attendances.application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.application.Queries
{
        public record GetAllAttendancesQuery(string UserName) : IRequest<IEnumerable<AttendanceResponse>>;

}
