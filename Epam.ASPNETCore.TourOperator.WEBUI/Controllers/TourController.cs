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
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace Epam.ASPNETCore.TourOperator.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;

        private readonly ITourLogic tourLogic;

        private readonly ICountryLogic countryLogic;

        private readonly IRegionLogic regionLogic;

        private readonly ICityLogic cityLogic;

        private readonly IMapper mapper;


        public TourController(ILogger<TourController> logger, 
            ITourLogic tourLogic, 
            ICountryLogic countryLogic,
            IRegionLogic regionLogic,
            ICityLogic cityLogic,
            IMapper mapper)
        {
            _logger = logger;
            this.tourLogic = tourLogic;
            this.countryLogic = countryLogic;
            this.regionLogic = regionLogic;
            this.cityLogic = cityLogic;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new SearchViewModel();

            var rand = new Random();
            var tours = tourLogic.GetTours().ToList();
            List<int> randomNumbers = new List<int>();
            Dictionary<string, int> toursCount = new Dictionary<string, int>();

            var tourCount = 3;
            for (int i = 0; i < tourCount; i++)
            {
                int number;
                do
                {
                    number = rand.Next(0, tours.Count());
                }
                while (randomNumbers.Contains(number));
                randomNumbers.Add(number);
                model.RandomTours.Add(mapper.Map<TourViewModel>(tours[number]));
            }
            model.Regions = new SelectList(regionLogic.GetRegions().ToList(), "Region_Id", "Title");
            model.Cities = new SelectList(cityLogic.GetCities().ToList(), "City_Id", "Title");
            model.Countries = new SelectList(countryLogic.GetCountries().ToList(), "Country_Id", "Title");
            foreach (var item in model.Countries)
            {
                model.ToursCount.Add(item.Text, tourLogic.GetToursByCountryId(Convert.ToInt32(item.Value)).ToArray().Length);
            }
            return View(model);
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

        public ActionResult GetRegionsByCountryId(int countryId)
        {
            return Json(regionLogic.GetRegionsByCountryId(countryId));
        }

        public ActionResult GetCitiesByRegionId(int regionId)
        {
            return Json(cityLogic.GetCitiesByRegionId(regionId));
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
