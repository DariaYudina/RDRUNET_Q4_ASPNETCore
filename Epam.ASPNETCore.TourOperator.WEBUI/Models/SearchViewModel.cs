﻿using Epam.ASPNETCore.TourOperator.Entities;
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
        private DateTime? endDate;
        private int? dateCount;
        private decimal? costStart;
        private decimal? costEnd;
        private DateTime? startDate;

        public List<string> RandomTourImages { get; set; } = new List<string>();
        public List<TourViewModel> RandomTours { get; set; } = new List<TourViewModel>();

        public List<CountryViewModel> ToursCount { get; set; } = new List<CountryViewModel>();

        public SelectList Countries { get; set; }

        public SelectList Regions { get; set; }

        public SelectList Areas { get; set; }

        public SelectList Cities { get; set; }

        [Display(Name = "Дата отъезда")]
        public DateTime? StartDate { get => startDate == DateTime.MinValue ? null : startDate; set => startDate = value; }
        public DateTime? EndDate { get => StartDate == null ? null : (DateTime?)(StartDate.Value.AddDays(DateCount ?? 0)); set => endDate = value; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Количество дней")]
        public int? DateCount { get => dateCount == 0 ? null : dateCount; set => dateCount = value; }

        [Range(0, Double.MaxValue)]
        [Display(Name = "Цена от")]
        public decimal? CostStart { get => costStart == 0 ? null : costStart; set => costStart = value; }

        [Range(0, Double.MaxValue)]
        [Display(Name = "Цена до")]
        public decimal? CostEnd { get => costEnd == 0 ? null : costEnd; set => costEnd = value; }

        public int? Country_Id { get; set; }

        public int? Region_Id { get; set; }

        public int? Area_Id { get; set; }

        public int? City_Id { get; set; }

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
