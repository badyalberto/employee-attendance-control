using AutoMapper;
using MediatR;
using ms.attendances.domain.Entities;
using ms.attendances.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.attendances.application.Command.Handlers
{
    public class CreateAttendanceCommandHandler : IRequestHandler<CreateAttendanceCommand,bool>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public CreateAttendanceCommandHandler(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendanceRecord = _mapper.Map<AttendanceRecord>(request.AttendaceRequest);
            attendanceRecord.UserName = request.UserName;

            return await _attendanceRepository.CreateAttendance(attendanceRecord);
        }
    }
}
