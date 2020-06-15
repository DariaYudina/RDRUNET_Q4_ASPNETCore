using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IBLL
{
    public interface ITourLogic
    {
        IEnumerable<Tour> GetTours();

        Tour GetTourById(int id);

        IEnumerable<Tour> GetToursByCountryId(int id);

        IEnumerable<Tour> GetToursBySearchParametrs(int? countryId, int? regionId, int? areaId, int? cityId, decimal? startCost,
            decimal? endCost, DateTime? startDate, int? dateCount);
    }
}
