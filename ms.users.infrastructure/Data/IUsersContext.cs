using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.infrastructure.Data
{
    public interface IUsersContext
    {
        IMapper GetMapper();
    }
}
