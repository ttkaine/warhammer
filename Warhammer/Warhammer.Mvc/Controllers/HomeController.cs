using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;
using Warhammer.Mvc.Models;

namespace Warhammer.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IAuthenticatedDataProvider data) : base(data)
        {
        }

        public ActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel
            {
                NewPages = DataProvider.NewPages().OrderByDescending(p => p.SignificantUpdate),
                UpdatedPages = DataProvider.ModifiedPages().OrderByDescending(p => p.SignificantUpdate),
                RecentChanges = DataProvider.RecentPages().ToList(),
                MyStuff = DataProvider.MyStuff().ToList(),
                MyPeople = DataProvider.MyPeople().ToList(),
                AllPeople = DataProvider.People().Where(m => !DataProvider.MyPeople().Contains(m)).OrderBy(m => m.FullName).ToList()
            };
            return View(model);
        }

        public ActionResult Sessions()
        {
            List<Session> sessions = DataProvider.Sessions().OrderByDescending(s => s.DateTime).ToList();
            return View(sessions);
        }

        public PartialViewResult PinnedItems()
        {
            List<Page> pages = DataProvider.PinnedPages().OrderBy(p => p.FullName).ToList();
            return PartialView(pages);
        }

        public ActionResult CharacterLeague()
        {
            List<Person> people = DataProvider.People().OrderByDescending(s => s.PointsValue).ThenByDescending(s => s.Modified).ToList();
            return View(people);
        }

        public ActionResult Graveyard()
        {
            List<Person> people = DataProvider.People().Where(p => p.IsDead).OrderBy(s => s.FullName).ToList();
            return View(people);
        }

        public ActionResult People()
        {
            List<Person> people = DataProvider.People().OrderBy(s => s.FullName).ToList();
            return View(people);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}