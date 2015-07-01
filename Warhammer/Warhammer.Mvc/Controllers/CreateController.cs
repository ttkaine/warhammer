using System;
using System.Web.Mvc;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;
using Warhammer.Mvc.Models;

namespace Warhammer.Mvc.Controllers
{
    public class CreateController : BaseController
    {
        // GET: Create
        public CreateController(IAuthenticatedDataProvider data) : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Person()
        {
            Person person = new Person();
            return View(person);
        }

        [HttpPost]
        public ActionResult Page(Page page)
        {
            if (ModelState.IsValid)
            {
                int personId = DataProvider.AddPage(page.ShortName, page.FullName, page.Description);
                return RedirectToAction("Index", "Page", new {id = personId});
            }
            return View(page);
        }

        public ActionResult Page()
        {
            Page page = new Page();
            return View(page);
        }

        [HttpPost]
        public ActionResult Person(Page page)
        {
            if (ModelState.IsValid)
            {
                int pageId = DataProvider.AddPerson(page.ShortName, page.FullName, page.Description);
                return RedirectToAction("Index", "Page", new { id = pageId });
            }
            return View(page);
        }

        public ActionResult GameSession()
        {
            Session session = new Session();
            return View(session);
        }

        [HttpPost]
        public ActionResult GameSession(Session session)
        {
            if (ModelState.IsValid)
            {
                if (!session.DateTime.HasValue)
                {
                    session.DateTime = DateTime.Now;
                }
                int sessionId = DataProvider.AddSession(session.FullName, session.ShortName, session.Description,
                    session.DateTime.Value);
                return RedirectToAction("Index", "Page", new { id = sessionId });
            }


            return View(session);
        }

        public ActionResult SessionLog(int? personid)
        {
            CreateSessionLogViewModel model = new CreateSessionLogViewModel
            {
                Person = new SelectList(DataProvider.People(), "Id", "ShortName"),
                Session = new SelectList(DataProvider.Sessions(), "Id", "ShortName")
                
            };
            SessionLog sessionLog = new SessionLog();
            if (personid.HasValue)
            {
                sessionLog.Person = DataProvider.GetPage(personid.Value) as Person;

                if (sessionLog.Person != null)
                {
                    model.SelectedPersonId = sessionLog.Person.Id;
                }
            }
            model.Log = sessionLog;
            return View(model);
        }

        [HttpPost]
        public ActionResult SessionLog(CreateSessionLogViewModel model)
        {

            if (ModelState.IsValid)
            {
               int logId = DataProvider.AddSessionLog(model.SelectedSessionId, model.SelectedPersonId, model.Log.ShortName, model.Log.FullName,model.Log.Description);
               return RedirectToAction("Index", "Page", new { id = logId });
            }


            model.Person = new SelectList(DataProvider.People(), "Id", "ShortName");
            model.Session = new SelectList(DataProvider.Sessions(), "Id", "ShortName");
            return View(model);
        }

    }
}