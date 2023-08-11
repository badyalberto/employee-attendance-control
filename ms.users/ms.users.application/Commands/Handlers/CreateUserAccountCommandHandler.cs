using MediatR;
using ms.users.domain.Entities;
using ms.users.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms.users.application.Commands.Handlers
{
    public class CreateUserAccountCommandHandler : IRequestHandler<CreateUserAccountCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserAccountCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<string> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CreateUser(
                new User(){ UserName = request.username, Password = request.password, Role = request.role}    
            );

            return user.UserName;
        }
    }
}
