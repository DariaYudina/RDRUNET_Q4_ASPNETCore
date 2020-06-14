using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.Entities
{
    public class Tour
    {
        private int dateCount;

        public int Tour_Id { get; set; }

        public decimal Cost { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Image { get; set; }

        public City City { get; set; }

        public Area Area { get; set; }

        public Region Region { get; set; }

        public Country Country { get; set; }

        public int DateCount { get => EndDate.Subtract(StartDate).Days; set => dateCount = value; }
    }
}
