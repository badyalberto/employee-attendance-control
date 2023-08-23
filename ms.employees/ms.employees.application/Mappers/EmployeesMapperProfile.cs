using AutoMapper;
using ms.employees.application.Responses;
using ms.employees.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Mappers
{
    public class EmployeesMapperProfile : Profile
    {
        public EmployeesMapperProfile() => 
            CreateMap<Employee,EmployeeResponse>().ReverseMap();
    }
}
