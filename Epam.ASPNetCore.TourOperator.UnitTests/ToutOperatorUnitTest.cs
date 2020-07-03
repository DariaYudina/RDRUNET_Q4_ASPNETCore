using Epam.ASPNETCore.TourOperator.BLL;
using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Epam.ASPNetCore.TourOperator.UnitTests
{
    [TestClass]
    public class ToutOperatorUnitTest
    {
        private RegionLogic _regionLogic;
        private AreaLogic _areaLogic;
        private CityLogic _cityLogic;
        private Mock<IRegionDao> _regionDaoMock;
        private Mock<IAreaDao> _areaDaoMock;
        private Mock<ICityDao> _cityDaoMock;

        [TestInitialize]
        public void Initialize()
        {
            _regionDaoMock = new Mock<IRegionDao>();
            _areaDaoMock = new Mock<IAreaDao>();
            _cityDaoMock = new Mock<ICityDao>();
            _regionLogic = new RegionLogic(_regionDaoMock.Object);
            _cityLogic = new CityLogic(_cityDaoMock.Object);
            _areaLogic = new AreaLogic(_areaDaoMock.Object);
        }

        [TestMethod]
        public void GetRegionsByCountry()
        {
            // Arrange
            List<Region> regions = new List<Region>();
            _regionDaoMock.Setup(b => b.GetRegionsByCountryId(It.IsAny<int>())).Returns(regions);

            // Act
            int countryId = 1;
            var res = _regionLogic.GetRegionsByCountryId(countryId);

            //Assert
            Assert.IsTrue(res == regions);
        }

        [TestMethod]
        public void GetAreasByRegion()
        {
            // Arrange
            List<Area> areas = new List<Area>();
            _areaDaoMock.Setup(b => b.GetAreasByRegionId(It.IsAny<int>())).Returns(areas);

            // Act
            int regionId = 1;
            var res = _areaLogic.GetAreasByRegionId(regionId);

            //Assert
            Assert.IsTrue(res == areas);
        }

        [TestMethod]
        public void GetCitiesByArea()
        {
            // Arrange
            List<City> cities = new List<City>();
            _cityDaoMock.Setup(b => b.GetCityiesByAreaId(It.IsAny<int>())).Returns(cities);

            // Act
            int areaId = 1;
            var res = _cityLogic.GetCitiesByAreaId(areaId);

            //Assert
            Assert.IsTrue(res == cities);
        }
    }
}
