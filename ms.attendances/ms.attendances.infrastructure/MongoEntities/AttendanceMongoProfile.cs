using AutoMapper;
using ms.attendances.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.infrastructure.MongoEntities
{
    public class AttendanceMongoProfile : Profile
    {
        public AttendanceMongoProfile() =>
            CreateMap<AttendanceMongo,AttendanceRecord>().ReverseMap();
    }
}
