using AutoMapper;
using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.WEBUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epam.ASPNETCore.TourOperator.WEBUI
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Tour, SearchViewModel>()
            //    .ForMember(dest => dest.CountryId, opt => opt.Ignore())
            //    .ForMember(dest => dest.RegionId, opt => opt.Ignore())
            //    .ForMember(dest => dest.CityId, opt => opt.Ignore());
            //CreateMap<Country, SelectListItem>()
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Country_Id.ToString()))
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title));
            CreateMap<Tour, TourViewModel>();
        }
    }
}
