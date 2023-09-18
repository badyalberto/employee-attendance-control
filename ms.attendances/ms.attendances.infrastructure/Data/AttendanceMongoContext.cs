using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ms.attendances.infrastructure.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.infrastructure.Data
{
    public class AttendanceMongoContext : IAttendanceContext
    {
        private readonly IConfiguration _configuration;
        private IMongoDatabase _mongoDatabase;

        public AttendanceMongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var bd = String.Concat( "mongodb://", configuration.GetConnectionString("Attendance:HostName"),
                                               ":", configuration.GetConnectionString("Attendance:Port"));
            //_mongoDatabase = new MongoClient("mongodb://mongo:27017").GetDatabase("DbHistoricalAttendance");

            _mongoDatabase = new MongoClient(String.Concat(
                                               "mongodb://", configuration.GetConnectionString("Attendance:HostName"),
                                               ":", configuration.GetConnectionString("Attendance:Port"))
                                           ).GetDatabase(configuration.GetConnectionString("Attendance:DataBase"));
        }

        public IMongoCollection<AttendanceMongo> AttendanceCollection =>
            _mongoDatabase.GetCollection<AttendanceMongo>(_configuration.GetConnectionString("Attendance:Collection"));
    }
}
