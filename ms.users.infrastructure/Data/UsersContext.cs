using Cassandra.Mapping;
using Microsoft.Extensions.Configuration;
using ms.users.infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.infrastructure.Data
{
    public class UsersContext
    {
        private IMapper _usersMapper { get; set; }
        private CassandraUserMapping _cassandraUserMapping { get; set; }
        public UsersContext(CassandraCluster cassandraCluster,
                            CassandraUserMapping cassandraUserMapping,
                            IConfiguration configuration) 
        {
            var keyspace = configuration.GetSection("DatabaseSettings:Keyspace").Value;
            var session = cassandraCluster.ConfiguredCluster.Connect(keyspace);
            _usersMapper = new Mapper(session);
            _cassandraUserMapping = cassandraUserMapping;
        }

        public IMapper GetMapper() => _usersMapper;
    }
}
