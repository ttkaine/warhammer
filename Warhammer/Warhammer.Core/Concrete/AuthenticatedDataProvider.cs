using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Abstract;
using Warhammer.Core.Abstract;

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
    }
}
