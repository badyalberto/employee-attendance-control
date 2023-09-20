using Dapper;
using ms.employees.domain.Entities;
using ms.employees.domain.Repositories;
using ms.employees.infrastructure.Data;
using ms.employees.infrastructure.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDapperContext _employeeContext;

        public EmployeeRepository(IDapperContext employeeContext) => _employeeContext = employeeContext;

        public async Task<string> CreateEmployee(Employee employee)
        {
            var response = await _employeeContext.Connection.ExecuteAsync(EmployeeDataSQL.Create,param:
                                                                            new {
                                                                                    UserName = employee?.UserName,
                                                                                    FirstName = employee?.FirstName,
                                                                                    LastName = employee?.LastName,
                                                                                    State = employee?.LastAttendanceState,
                                                                                    Notes = employee?.LastAttendanceNotes
                                                                            });
            return response > 0 ? employee.UserName : throw new Exception("Usuario no creado!");
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var response = await _employeeContext.Connection.QueryAsync<Employee>(EmployeeDataSQL.GetAll);
            return response?.ToList() ?? new List<Employee>();
        }

        public Task<Employee> GetEmployee(string userName)
        {
            var res = _employeeContext.Connection.QueryFirstOrDefaultAsync<Employee>(EmployeeDataSQL.GetEmployee,
                param: new { UserName = userName });

            return res;
        }

        public async Task<string> UpdateAttendanceStateEmployee(string userName, bool attendance, string notes)
        {
            var response = await _employeeContext.Connection.ExecuteAsync(EmployeeDataSQL.UpdateAttendanceState,param:
                                                                    new { UserName = userName, State = attendance, Notes = notes });

            return response > 0 ? userName : null;
        }
    }
}
