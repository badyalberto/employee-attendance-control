using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.infrastructure.SQLData
{
    public class EmployeeDataSQL
    {
        public const string GetAll = @"SELECT empl_username UserName,
                                              empl_firstname FirstName,
                                              empl_lastname LastName,
                                              empl_lastattendance LastAttendanceDate,
                                              empl_state LastAttendanceState,
                                              empl_notes LastAttendanceNotes 
                                        FROM dbo.Employee
                                        ORDER BY empl_username";

        public const string Create = @"INSERT dbo.Employee (empl_username,
                                                            empl_firstname,
                                                            empl_lastname,
                                                            empl_lastattendance,
                                                            empl_state,
                                                            empl_notes)
                                        VALUES(@UserName,@FirstName,@LastName,GetDate(),@State,@Notes)";

        public const string UpdateAttendanceState = @"UPDATE dbo.Employee
                                                      SET empl_lastattendance=GetDate(),
                                                      empl_state=@State,
                                                      empl_notes=@Notes
                                                      WHERE empl_username=@UserName";

        public const string GetEmployee = @"SELECT TOP 1 empl_username UserName,
                                              empl_firstname FirstName,
                                              empl_lastname LastName,
                                              empl_lastattendance LastAttendanceDate,
                                              empl_state LastAttendanceState,
                                              empl_notes LastAttendanceNotes 
                                        FROM dbo.Employee
                                        WHERE empl_username=@UserName";
    }
}
