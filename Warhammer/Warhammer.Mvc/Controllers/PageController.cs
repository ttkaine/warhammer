using System.IO;
using System.Web.Mvc;
using Warhammer.Core.Abstract;
using Page = Warhammer.Core.Entities.Page;

namespace Warhammer.Mvc.Controllers
{
    public class PageController : BaseController
    {
        
        // GET: Page
        public PageController(IAuthenticatedDataProvider data) : base(data)
        {
        }

        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                Page page = DataProvider.GetPage(id.Value);
                if (page != null)
                {
                    return View(page);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(Page page)
        {
            if (ModelState.IsValid)
            {
                Page updatedPage = DataProvider.UpdatePageDetails(page.Id, page.ShortName, page.FullName, page.Description);
                return View(updatedPage);
            }
            return View(page);
        }

        public ActionResult Image(int id)
        {
            Page page = DataProvider.GetPage(id);
            var defaultDir = Server.MapPath("/Content/Images");

            if (page != null)
            {
                if (page.ImageData != null && page.ImageData.Length > 100 && !string.IsNullOrWhiteSpace(page.ImageMime))
                {
                    return File(page.ImageData, page.ImageMime);
                }
                //var imagePath = Path.Combine(defaultDir, page.GetType() + ".jpg");
                //return File(imagePath, "image/jpeg");
            }

            var defaultImagePath = Path.Combine(defaultDir, "no-image.jpg");
            return File(defaultImagePath, "image/jpeg"); 
        }
    }
}