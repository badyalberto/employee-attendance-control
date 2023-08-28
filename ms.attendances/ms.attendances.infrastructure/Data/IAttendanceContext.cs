using MongoDB.Driver;
using ms.attendances.infrastructure.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.infrastructure.Data
{
    public interface IAttendanceContext
    {
        IMongoCollection<AttendanceMongo> AttendanceCollection { get; }
    }
}
