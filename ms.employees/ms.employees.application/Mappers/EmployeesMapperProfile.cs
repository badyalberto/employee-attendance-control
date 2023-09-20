using AutoMapper;
using ms.employees.application.Commands;
using ms.employees.application.Responses;
using ms.employees.domain.Entities;
using ms.rabbitmq.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Mappers
{
    public class EmployeesMapperProfile : Profile
    {
        public EmployeesMapperProfile()
        {
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
            CreateMap<CreateEmployeeCommand, EmployeeCreateEvent>().ReverseMap();
            CreateMap<Employee, AttendanceStateChangedEvent>()
                .ForMember(dest => dest.Attendance, source => source.MapFrom(source => source.LastAttendanceState))
                .ForMember(dest => dest.Date, source => source.MapFrom(source => source.LastAttendanceDate))
                .ForMember(dest => dest.Notes, source => source.MapFrom(source => source.LastAttendanceNotes))
                .ForMember(dest => dest.FullName, source => source.MapFrom(source => String.Concat(source.FirstName," ",source.LastName))).ReverseMap();
        }
           
    }
}
