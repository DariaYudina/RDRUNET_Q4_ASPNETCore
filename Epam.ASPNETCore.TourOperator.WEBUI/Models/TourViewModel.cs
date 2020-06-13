using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epam.ASPNETCore.TourOperator.WEBUI.Models
{
    public class TourViewModel
    {
        private int dateCount;
        private string image;

        public int Tour_Id { get; set; }

        public string Image { get => "data:image/jpg;base64," + image ; set => image = value; }
    }
}
