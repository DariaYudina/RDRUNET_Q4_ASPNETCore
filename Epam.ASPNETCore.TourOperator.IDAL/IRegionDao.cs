using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IDAL
{
    public interface IRegionDao
    {
        IEnumerable<Region> GetRegions();
        Region GetRegionById(int id);
    }
}
