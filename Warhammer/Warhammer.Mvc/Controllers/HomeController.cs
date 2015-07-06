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