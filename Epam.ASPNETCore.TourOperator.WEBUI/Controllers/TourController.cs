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
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Epam.ASPNETCore.TourOperator.Entities;

namespace Epam.ASPNETCore.TourOperator.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;

        private readonly ITourLogic tourLogic;

        private readonly ICountryLogic countryLogic;

        private readonly IRegionLogic regionLogic;

        private readonly IAreaLogic areaLogic;

        private readonly ICityLogic cityLogic;

        private readonly IMapper mapper;

        private readonly IDistributedCache distributedCache;

        public TourController(ILogger<TourController> logger, 
            ITourLogic tourLogic, 
            ICountryLogic countryLogic,
            IRegionLogic regionLogic,
            IAreaLogic areaLogic,
            ICityLogic cityLogic,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            this.tourLogic = tourLogic;
            this.countryLogic = countryLogic;
            this.regionLogic = regionLogic;
            this.areaLogic = areaLogic;
            this.cityLogic = cityLogic;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            List<int> toursId = new List<int>();
            var tours = new List<Tour>();
            if (string.IsNullOrEmpty(distributedCache.GetString("tours")))
            {
                var cachetours = tourLogic.GetTours().ToList();
                toursId = cachetours.Select(p => p.Tour_Id).ToList();
                var tourString = JsonConvert.SerializeObject(toursId);
                distributedCache.SetString("tours", tourString);
            }
            else
            {
                toursId = JsonConvert.DeserializeObject<List<int>>(distributedCache.GetString("tours"));
                foreach (var item in toursId)
                {
                    tours.Add(tourLogic.GetTourById(item));
                }
            }

            var model = new SearchViewModel();
            var rand = new Random();
            //var tours = tourLogic.GetTours().ToList();
            List<int> randomNumbers = new List<int>();

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

            model.Areas = new SelectList(areaLogic.GetAreas().ToList(), "Area_Id", "Title");
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
        public IActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var rand = new Random();
                var tours = tourLogic.GetTours().ToList();
                List<int> randomNumbers = new List<int>();
               
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

                model.Areas = new SelectList(areaLogic.GetAreas().ToList(), "Area_Id", "Title");
                model.Regions = new SelectList(regionLogic.GetRegions().ToList(), "Region_Id", "Title");
                model.Cities = new SelectList(cityLogic.GetCities().ToList(), "City_Id", "Title");
                model.Countries = new SelectList(countryLogic.GetCountries().ToList(), "Country_Id", "Title");
                
                foreach (var item in model.Countries)
                {
                    model.ToursCount.Add(item.Text, tourLogic.GetToursByCountryId(Convert.ToInt32(item.Value)).ToArray().Length);
                }

                if(model.Area_Id == null || model.City_Id == null || model.Region_Id == null || model.Country_Id == null)
                {
                    return View("SearchResult", mapper.Map<List<TourViewModel>>(
                            tourLogic.GetToursBySearchParametrs(model.Country_Id, model.Region_Id, model.Area_Id, 
                            model.City_Id,model.CostStart, model.CostEnd, model.StartDate, model.DateCount).ToList()));
                }

                return View(model);
            }

            return View("SearchResult", mapper.Map<List<TourViewModel>>(
                    tourLogic.GetToursBySearchParametrs(model.Country_Id, model.Region_Id, model.Area_Id,
                    model.City_Id, model.CostStart, model.CostEnd, model.StartDate, model.DateCount).ToList()));
        }

        public ActionResult GetRegionsByCountryId(int countryId = 0)
        {
            return Json(regionLogic.GetRegionsByCountryId(countryId));
        }

        public ActionResult GetAreasByRegionId(int? regionId)
        {
            return Json(areaLogic.GetAreasByRegionId(regionId));
        }

        public ActionResult GetCitiesByAreaId(int areaId = 0)
        {
            return Json(cityLogic.GetCitiesByAreaId(areaId));
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

        //[HttpPost]
        //public IActionResult Index(CreatePost model)
        //{
        //    var img = model.MyImage;
        //    var imgCaption = model.ImageCaption;
        //    var fileName = Path.GetFileName(model.MyImage.FileName);
        //    var contentType = model.MyImage.ContentType;
        //    string imgBase64;
        //    byte[] bytearr;

        //    if (model.MyImage != null)
        //    {
        //        bytearr = GetByteArrayFromImage(model.MyImage);
        //        imgBase64 = Convert.ToBase64String(bytearr);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
