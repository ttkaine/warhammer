using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeImage(string saveAction, int id, HttpPostedFileBase profileImageFile, double? y1, double? x1, double? h, double? w)
        {
            if (ModelState.IsValid)
            {
                if (saveAction == "Save")
                {
                    Rectangle cropArea = GetCropArea(y1, x1, h, w);
                    if (profileImageFile != null)
                    {
                        Image theImage = System.Drawing.Image.FromStream(profileImageFile.InputStream, true, true);
                        Image croppedImage = _imageProcessor.Crop(theImage, cropArea);
                        croppedImage = _imageProcessor.ResizeImage(croppedImage, new Size {Height = 200, Width = 200});
                        byte[] imageData = _imageProcessor.GetJpegFromImage(croppedImage);

                        DataProvider.ChangePicture(id, imageData, "Image/Jpeg");
                        return RedirectToAction("Index", new {id = id});
                    }
                }
                if (saveAction == "Remove Image")
                {
                    DataProvider.RemoveProfileImage(id);
                    return RedirectToAction("Index", new { id = id });
                }
            }
            // there is something wrong with the data values
            Page model = DataProvider.GetPage(id);
            return View(model);
        }
        private Rectangle GetCropArea(double? y1, double? x1, double? h, double? w)
        {
            if (y1.HasValue && x1.HasValue && h.HasValue && w.HasValue)
            {
                return new Rectangle((int)x1.Value, (int)y1.Value, (int)w.Value, (int)h.Value);
            }

            return new Rectangle();
        }
    }
}