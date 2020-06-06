using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.BLL
{
    public class CountryLogic : ICountryLogic
    {
        private readonly ICountryDao countryDao;
        public CountryLogic(ICountryDao countryDao)
        {
            this.countryDao = countryDao;
        }

        public IEnumerable<Country> GetCountries()
        {
            return countryDao.GetCountries();
        }
    }
}
