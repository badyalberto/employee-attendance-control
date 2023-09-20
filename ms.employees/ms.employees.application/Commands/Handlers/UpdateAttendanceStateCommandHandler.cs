using AutoMapper;
using MediatR;
using ms.employees.application.HttpCommunications;
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
    public class UpdateAttendanceStateCommandHandler : IRequestHandler<UpdateAttendanceStateCommand,string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProducer _producer;
        private readonly IMapper _mapper;
        private readonly IAttendanceApiCommunication _attendanceApiCommunication;

        public UpdateAttendanceStateCommandHandler(IEmployeeRepository employeeRepository,IProducer producer,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _producer = producer;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateAttendanceStateCommand request, CancellationToken cancellationToken)
        {
            var userAttendances = await _attendanceApiCommunication.GetAllAttendances(request.UserName, request.token);

            var numberOfAttendances = userAttendances.Count();

            string notes = request.Notes == null ? $"[{numberOfAttendances} Asistencias]" : string.Concat(request.Notes, $"[{numberOfAttendances} Asistencias]");

            var res = await _employeeRepository.UpdateAttendanceStateEmployee(request.UserName,request.Attendance,request.Notes);

            var employee = await _employeeRepository.GetEmployee(request.UserName);

            _producer.Produce(_mapper.Map<AttendanceStateChangedEvent>(employee));
            return res;
        }
    }
}
