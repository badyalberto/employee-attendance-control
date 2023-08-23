using ms.employees.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<string> UpdateAttendanceStateEmployee(string userName, bool attendance, string notes);
        Task<string> CreateEmployee(Employee employee);
    }
}
