using ms.employees.application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.Repositories
{
    public interface IEmploteeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<string> UpdateAttendanceStateEmployee(string userName, bool attendance, string notes);
        Task<string> CreateEmployee(Employee employee);
    }
}
