using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IBLL
{
    public interface IRegionLogic
    {
        IEnumerable<Region> GetRegions();
        Region GetRegionById(int id);
        IEnumerable<Region> GetRegionsByCountryId(int id);
    }
}
