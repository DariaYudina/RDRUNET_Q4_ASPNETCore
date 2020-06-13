using Epam.ASPNETCore.TourOperator.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epam.ASPNETCore.TourOperator.WEBUI.Models
{
    public class SearchViewModel
    {
        public List<string> RandomTourImages { get; set; } = new List<string>();
        public List<TourViewModel> RandomTours { get; set; } = new List<TourViewModel>();
        public SelectList Countries { get; set; }
        public SelectList Regions { get; set; } 
        public SelectList Cities { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Cost { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
    }
}
