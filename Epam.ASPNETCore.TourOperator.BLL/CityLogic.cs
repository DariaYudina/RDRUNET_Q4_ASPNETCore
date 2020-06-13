using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.BLL
{
    public class CityLogic : ICityLogic
    {
        private readonly ICityDao cityDao;
        public CityLogic(ICityDao cityDao)
        {
            this.cityDao = cityDao;
        }

        public IEnumerable<City> GetCities()
        {
            return cityDao.GetCities();
        }

        public City GetCityById(int id)
        {
            return cityDao.GetCityById(id);
        }

        public IEnumerable<City> GetCitiesByRegionId(int id)
        {
            return cityDao.GetCityiesByRegionId(id);
        }
    }
}
