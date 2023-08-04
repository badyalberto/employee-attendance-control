using Cassandra.Mapping;
using ms.users.domain.Entities;
using ms.users.domain.Interfaces;
using ms.users.infrastructure.CQLRepositories;
using ms.users.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _usersMapper;
        public UserRepository(IUsersContext usersContext) => _usersMapper = usersContext.GetMapper();

        public async Task<List<User>> GetAllUsers() => await Task.FromResult(_usersMapper.Fetch<User>()?.ToList());
        
        public async Task<User> GetUser(string userName, string password)
        {
            var user = await _usersMapper.FirstOrDefaultAsync<User>(UserCQL.GetUserByUserNameCql,userName.ToLowerInvariant());

            if (user == null) throw new Exception("El usuario no existe.");

            if (password != user.Password) throw new Exception("Las credenciales no son correctas.");

            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            var appliedInfo = await _usersMapper.InsertIfNotExistsAsync<User>(user);

            if (!appliedInfo.Applied) throw new Exception($"El usuario {user.UserName} ya existe en la BBDD.");

            return user;
        }
    }
}
