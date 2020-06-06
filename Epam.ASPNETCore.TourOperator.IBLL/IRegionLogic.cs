using Epam.ASPNETCore.TourOperator.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.IBLL
{
    public interface IRegionLogic
    {
        IEnumerable<Region> GetRegiones();
    }
}
