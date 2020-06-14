using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IBLL
{
    public interface IAreaLogic
    {
        IEnumerable<Area> GetAreas();
        Area GetAreaById(int id);
        IEnumerable<Area> GetAreasByRegionId(int? id);
    }
}
