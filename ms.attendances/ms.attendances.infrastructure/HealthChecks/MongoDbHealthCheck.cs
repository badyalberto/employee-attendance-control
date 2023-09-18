using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.attendances.infrastructure.HealthChecks
{
    public class MongoDbHealthCheck : IHealthCheck
    {
        private IMongoDatabase _db { get; set; }
        public MongoClient _mongoClient { get; set; }

        public MongoDbHealthCheck()
        {
            _mongoClient = new MongoClient("mongodb://mongo:27017");

            _db = _mongoClient.GetDatabase("DbHistoricalAttendance");

        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var healthCheck = await _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");

                if (healthCheck != null) { return HealthCheckResult.Healthy(); }

                return HealthCheckResult.Unhealthy();
            }
            catch (Exception ex) { return HealthCheckResult.Unhealthy(); }
        }
    }
}
