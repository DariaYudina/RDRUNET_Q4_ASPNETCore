using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Epam.ASPNETCore.TourOperator.Models;
using Epam.ASPNETCore.TourOperator.IBLL;

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
            var r = tourLogic.GetTours();
            return View();
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
    }
}
