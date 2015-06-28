using System.Web.Mvc;
using Warhammer.Core.Abstract;

namespace Warhammer.Mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly IAuthenticatedDataProvider _data;

        protected IAuthenticatedDataProvider DataProvider
        {
            get { return _data; }
        }

        public BaseController(IAuthenticatedDataProvider data)
        {
            _data = data;
        }
    }
}