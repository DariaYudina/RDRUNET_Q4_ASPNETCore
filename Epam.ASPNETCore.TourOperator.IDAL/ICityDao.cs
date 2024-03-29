﻿using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IDAL
{
    public interface ICityDao
    {
        IEnumerable<City> GetCities();
        City GetCityById(int id);
        IEnumerable<City> GetCityiesByAreaId(int id);
    }
}
