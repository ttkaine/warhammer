using System.Web.Mvc;
using Warhammer.Core.Abstract;
using Warhammer.Core.Entities;

namespace Warhammer.Mvc.Controllers
{
    public class CreateController : Controller
    {
        private IAuthenticatedDataProvider _data;

        public CreateController(IAuthenticatedDataProvider data)
        {
            _data = data;
        }

        // GET: Create
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
        public ActionResult Person(Person person)
        {
            if (ModelState.IsValid)
            {
                int personId = _data.AddPerson(person.ShortName, person.FullName, person.Description);
                return RedirectToAction("Index", "Page", new {id = personId});
            }
            return View(person);
        }

        public ActionResult SessionLog(int? personid)
        {
            throw new System.NotImplementedException();
        }
    }
}