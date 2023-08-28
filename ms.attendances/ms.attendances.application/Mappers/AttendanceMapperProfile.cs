using AutoMapper;
using ms.attendances.application.Request;
using ms.attendances.application.Responses;
using ms.attendances.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.application.Mappers
{
    public class AttendanceMapperProfile : Profile
    {
        public AttendanceMapperProfile() { 
            CreateMap<AttendanceRecord,AttendanceResponse>().ReverseMap();
            CreateMap<AttendanceRecord,CreateAttendanceRequest>().ReverseMap();

        }
    }
}
