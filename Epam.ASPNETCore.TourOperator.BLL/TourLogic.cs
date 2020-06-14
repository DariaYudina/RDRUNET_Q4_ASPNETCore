using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.BLL
{
    public class TourLogic : ITourLogic
    {
        private readonly ITourDao tourDao;

        public TourLogic(ITourDao tourDao)
        {
            this.tourDao = tourDao;
        }

        public Tour GetTourById(int id)
        {
            return tourDao.GetTourById(id);
        }

        public IEnumerable<Tour> GetTours()
        {
            return tourDao.GetTours();
        }

        public IEnumerable<Tour> GetToursByCountryId(int id)
        {
            return tourDao.GetToursByCountryId(id);
        }

        public IEnumerable<Tour> GetToursBySearchParametrs(int? countryId, int? regionId, int? areaId, int? cityId, decimal? startCost, decimal? endCost, DateTime startDate, int dateCount)
        {
            return tourDao.GetToursBySearchParametrs(countryId, regionId, areaId, cityId, startCost, endCost, startDate, dateCount); 
        }
    }
}
