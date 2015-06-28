using System.Linq;
using System.Web.Mvc;
using Warhammer.Core.Abstract;
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
                MyPeople = DataProvider.MyPeople().ToList()
            };
            return View(model);
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