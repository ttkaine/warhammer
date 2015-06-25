using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Abstract;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Concrete
{
    public class AuthenticatedDataProvider : IAuthenticatedDataProvider
    {
        private readonly IAuthenticatedUserProvider _authenticatedUser;
        private readonly IRepository _repository;
        private readonly IModelFactory _factory;

        public AuthenticatedDataProvider(IAuthenticatedUserProvider authenticatedUser, IRepository repository, IModelFactory factory)
        {
            _authenticatedUser = authenticatedUser;
            _repository = repository;
            _factory = factory;

            if (!_authenticatedUser.UserIsAuthenticated)
            {
                throw new Exception("User is not Authenticated");
            }
        }

        public ICollection<Person> MyPeople()
        {
            return _repository.People().Where(p => p.Player.UserName == _authenticatedUser.UserName).ToList();
        }

        public void AddLog(int sessionId, int personId, string title, string description)
        {
            
        }
    }
}
