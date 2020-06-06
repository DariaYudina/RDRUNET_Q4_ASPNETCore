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

        private readonly ICountryLogic countryLogic;

        private readonly IRegionLogic regionLogic;

        private readonly ICityLogic cityLogic;


        public TourController(ILogger<TourController> logger, 
            ITourLogic tourLogic, 
            ICountryLogic countryLogic,
            IRegionLogic regionLogic,
            ICityLogic cityLogic)
        {
            _logger = logger;
            this.tourLogic = tourLogic;
            this.countryLogic = countryLogic;
            this.regionLogic = regionLogic;
            this.cityLogic = cityLogic;
        }

        public IActionResult Index()
        {
            var r = tourLogic.GetTours().ToList().FirstOrDefault().Image;
            //var r2 = cityLogic.GetCities();
            return View(new CreatePost() { Image = r});
            //return View();
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

            //ViewBag.Companies = new SelectList(companies, "Id", "Name");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CreatePost model)
        {
            return View();
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
