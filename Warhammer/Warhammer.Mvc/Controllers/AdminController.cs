using System.Web.Mvc;
using Warhammer.Core.Abstract;

namespace Warhammer.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IDatabaseUpdateProvider _databaseUpdate;

        // GET: Admin
        public AdminController(IAuthenticatedDataProvider data, IDatabaseUpdateProvider databaseUpdate) : base(data)
        {
            _databaseUpdate = databaseUpdate;
        }

        public ActionResult DbUpdate()
        {
            string folder = Server.MapPath(Url.Content("~/Content/DbUpdateScripts/"));
            bool did = _databaseUpdate.PerformUpdates(folder);
            return View(did);
        }
    }
}