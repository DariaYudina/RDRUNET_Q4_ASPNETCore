using Epam.ASPNETCore.TourOperator.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Epam.ASPNETCore.TourOperator.WEBUI.Models
{
    public class SearchViewModel : IValidatableObject
    {
        private DateTime endDate;

        public List<string> RandomTourImages { get; set; } = new List<string>();
        public List<TourViewModel> RandomTours { get; set; } = new List<TourViewModel>();

        public Dictionary<string, int> ToursCount { get; set; } = new Dictionary<string, int>();

        public SelectList Countries { get; set; }
        public SelectList Regions { get; set; }
        public SelectList Cities { get; set; }

        [Display(Name = "Дата отъезда")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get => StartDate.AddDays(DateCount); set => endDate = value; }

        [Display(Name = "Количество дней")]
        public int DateCount { get; set; }

        [Range(0, Double.MaxValue)]
        [Display(Name = "Цена от")]
        public decimal CostStart { get; set; }

        [Range(0, Double.MaxValue)]
        [Display(Name = "Цена до")]
        public decimal CostEnd { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (CostStart > CostEnd)
            {
                errors.Add(new ValidationResult("Цена до не должна быть меньше цены от!", new List<string>() { "CostEnd", "CostStart" }));
                errors.Add(new ValidationResult("Цена до не должна быть меньше цены от!"));
            }

            return errors;
        }
    }
}
