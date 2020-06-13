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
    }
}
