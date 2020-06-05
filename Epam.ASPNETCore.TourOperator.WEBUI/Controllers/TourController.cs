using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Epam.ASPNETCore.TourOperator.Models;
using Epam.ASPNETCore.TourOperator.IBLL;
using Microsoft.AspNetCore.Http;
using System.IO;
using Epam.ASPNETCore.TourOperator.WEBUI.Models;

namespace Epam.ASPNETCore.TourOperator.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;

        private readonly ITourLogic tourLogic;

        public TourController(ILogger<TourController> logger, ITourLogic tourLogic)
        {
            _logger = logger;
            this.tourLogic = tourLogic;
        }

        public IActionResult Index()
        {
            var r = tourLogic.GetTours().ToList().FirstOrDefault().Image;
            return View(new CreatePost() { Image = r});
        }

        [HttpPost]
        public IActionResult Index(CreatePost model)
        {
            var img = model.MyImage;
            var imgCaption = model.ImageCaption;
            var fileName = Path.GetFileName(model.MyImage.FileName);
            var contentType = model.MyImage.ContentType;
            string imgBase64;
            byte[] bytearr;

            if (model.MyImage != null)
            {
                bytearr = GetByteArrayFromImage(model.MyImage);
                imgBase64 = Convert.ToBase64String(bytearr);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
