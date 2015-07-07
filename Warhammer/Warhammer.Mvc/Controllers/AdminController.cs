using System;
using System.Web.Mvc;
using System.Web.UI;
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

        public ActionResult DeletePage(int id)
        {
            Core.Entities.Page page = DataProvider.GetPage(id);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(page);
            }
        }

        [HttpPost]
        public ActionResult DeletePage(Core.Entities.Page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataProvider.DeletePage(page.Id);
                }
                catch (Exception ex)
                {
                    return View("DeleteError", ex);
                }
                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}