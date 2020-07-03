using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epam.ASPNETCore.TourOperator.WEBUI.Models
{
    public class CreatePost
    {
        public string ImageCaption { set; get; }
        public string ImageDescription { set; get; }
        public IFormFile MyImage { set; get; }
      
        private string image;
        public string Image { set => image = value; get => "data:image/jpg;base64," + image; }

    }
}
