using System;
using System.Linq;
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

        public ActionResult KillPerson(int id)
        {
            Core.Entities.Person person = DataProvider.People().FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(person);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KillPerson(Core.Entities.Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (person.IsDead)
                    {
                        DataProvider.ResurrectPerson(person.Id);
                    }
                    else
                    {
                        DataProvider.KillPerson(person.Id, person.Obiturary);
                    }
                    
                }
                catch (Exception ex)
                {
                    return View("Killing Error", ex);
                }

            }
            return RedirectToAction("Graveyard", "Home");
        }


        public ActionResult PinPage(int id)
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
        public ActionResult PinPage(Core.Entities.Page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataProvider.PinPage(page.Id);
                }
                catch (Exception ex)
                {
                    return View("Pinning Error", ex);
                }

            }
            return RedirectToAction("Index", "Home");
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