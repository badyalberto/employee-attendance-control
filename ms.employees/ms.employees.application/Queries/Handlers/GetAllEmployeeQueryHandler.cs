using AutoMapper;
using MediatR;
using ms.employees.application.Responses;
using ms.employees.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.employees.application.Queries.Handlers
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IEnumerable<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        

        public async Task<IEnumerable<EmployeeResponse>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
        }
    }
}
