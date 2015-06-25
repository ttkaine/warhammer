using Warhammer.Core.Abstract;

namespace Warhammer.Core.Concrete
{
    public class PublicDataProvider : IPublicDataProvider
    {
        private readonly IRepository _repository;
        private readonly IModelFactory _factory;

        public PublicDataProvider(IRepository repository, IModelFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }
    }
}
