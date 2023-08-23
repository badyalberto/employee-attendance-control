using MediatR;
using ms.employees.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.employees.application.Commands.Handlers
{
    public class UpdateAttendanceStateCommandHandler : IRequestHandler<UpdateAttendanceStateCommand,string>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateAttendanceStateCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> Handle(UpdateAttendanceStateCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.UpdateAttendanceStateEmployee(request.UserName,request.Attendance,request.Notes);
        }
    }
}
