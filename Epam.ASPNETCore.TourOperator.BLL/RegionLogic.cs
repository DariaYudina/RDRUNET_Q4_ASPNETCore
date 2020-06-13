using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.BLL
{
    public class RegionLogic : IRegionLogic
    {
        private readonly IRegionDao regionDao;
        public RegionLogic(IRegionDao regionDao)
        {
            this.regionDao = regionDao;
        }

        public IEnumerable<Region> GetRegions()
        {
            return regionDao.GetRegions();
        }
    }
}
