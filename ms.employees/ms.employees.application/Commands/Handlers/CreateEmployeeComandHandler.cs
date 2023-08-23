using MediatR;
using ms.employees.domain.Entities;
using ms.employees.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.employees.application.Commands.Handlers
{
    public class CreateEmployeeComandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeComandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.CreateEmployee(new Employee { UserName = request.UserName,
                                                                           FirstName = request.FirstName,
                                                                           LastName = request.LastName});
        }
    }
}
