using ms.attendances.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.domain.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceRecord>> GetAllAttendances(string userName);
        Task<bool> CreateAttendance(AttendanceRecord entity);
    }
}
