using AutoMapper;
using MediatR;
using ms.employees.domain.Entities;
using ms.employees.domain.Repositories;
using ms.rabbitmq.Events;
using ms.rabbitmq.Producers;
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
        private readonly IProducer _producer;
        private readonly IMapper _mapper;

        public CreateEmployeeComandHandler(IEmployeeRepository employeeRepository, IProducer producer, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _producer = producer;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var res =  await _employeeRepository.CreateEmployee(new Employee { UserName = request.UserName,
                                                                           FirstName = request.FirstName,
                                                                           LastName = request.LastName});
            _producer.Produce(_mapper.Map<EmployeeCreateEvent>(request));

            return res;
        }
    }
}
