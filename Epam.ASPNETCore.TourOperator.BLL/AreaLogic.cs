using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.BLL
{
    public class AreaLogic : IAreaLogic
    {
        private readonly IAreaDao areaDao;
        public AreaLogic(IAreaDao areaDao)
        {
            this.areaDao = areaDao;
        }

        public Area GetAreaById(int id)
        {
            return areaDao.GetAreaById(id);
        }

        public IEnumerable<Area> GetAreas()
        {
            return areaDao.GetAreas();
        }

        public IEnumerable<Area> GetAreasByRegionId(int? id)
        {
            return areaDao.GetAreasByRegionId(id);
        }
    }
}
