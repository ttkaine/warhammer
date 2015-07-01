using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Warhammer.Core.Abstract;
using Page = Warhammer.Core.Entities.Page;

namespace Warhammer.Mvc.Controllers
{
    public class PageController : BaseController
    {
        private IImageProcessor _imageProcessor;
        
        // GET: Page
        public PageController(IAuthenticatedDataProvider data, IImageProcessor imageProcessor) : base(data)
        {
            _imageProcessor = imageProcessor;
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
        public ActionResult Index(Page page, string saveAction)
        {
            if (ModelState.IsValid && saveAction == "Save")
            {
                Page updatedPage = DataProvider.UpdatePageDetails(page.Id, page.ShortName, page.FullName, page.Description);
                if (updatedPage.ImageData == null)
                {
                    Image image = _imageProcessor.GetImageFromHtmlString(updatedPage.Description);
                    if (image != null)
                    {
                        image = _imageProcessor.ResizeImage(image, new Size {Height = 200, Width = 200});
                        byte[] imageData = _imageProcessor.GetJpegFromImage(image);
                        
                        DataProvider.ChangePicture(updatedPage.Id, imageData, "Image/Jpeg");
                    }
                }
                return View(updatedPage);
            }
            return RedirectToAction("index", new { id = page.Id });
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

        public ActionResult ChangeImage(int? id)
        {
            if (id.HasValue)
            {
                Page page = DataProvider.GetPage(id.Value);
                if (page != null)
                {
                    return View(page);
                }               
            }


            return RedirectToAction("index", "home");
        }

    }
}