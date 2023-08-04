using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.infrastructure.CQLRepositories
{
    public static class UserCQL
    {
        public const string GetUserByUserNameCql = "SELECT * FROM User WHERE user_username = ?";
    }
}
