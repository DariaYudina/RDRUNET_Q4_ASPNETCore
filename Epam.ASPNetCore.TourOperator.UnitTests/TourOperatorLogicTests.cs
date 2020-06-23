using Epam.ASPNETCore.TourOperator.BLL;
using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Epam.ASPNetCore.TourOperator.UnitTests
{
    [TestClass]
    class TourOperatorLogicTests
    {

        private RegionLogic _regionLogic;
        private Mock<IRegionDao> _regionDaoMock;
        private CityLogic _cityLogic;
        private Mock<ICityDao> _cityDaoMock;

        [TestInitialize]
        public void Initialize()
        {
            _regionDaoMock = new Mock<IRegionDao>();
            _regionLogic = new RegionLogic(_regionDaoMock.Object);
            _cityDaoMock = new Mock<ICityDao>();
            _cityLogic = new CityLogic(_cityDaoMock.Object);
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
        public void GetCitiesByArea()
        {
            // Arrange
            List<City> regions = new List<City>();
            _cityDaoMock.Setup(b => b.GetCityiesByAreaId(It.IsAny<int>())).Returns(regions);

            // Act
            int areaId = 1;
            var res = _cityDaoMock.g(areaId);

            //Assert
            Assert.IsTrue(res == regions);
        }

    }
}
