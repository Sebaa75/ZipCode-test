using System;
using GeoLib.Contracts;
using GeoLib.Data.Entities;
using GeoLib.Data.RepositoryInterfaces;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
    [TestClass]
    public class GeoLibServiceTest
    {
        [TestMethod]
        public void Test_ServiceOperation_GetZipInfo()
        {
            //Arrange - zaranzuj sztuczne repozytorium, ktore zwraca sztuczne dane
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

            const string ZIP = "80200";



            ZipCode zipCode = new ZipCode()
            {
                City = "Gdansk",
                State = new State() { Abbreviation = "PR" },
                Zip = "80200"
            };

            mockZipCodeRepository.Setup(mr => mr.GetByZip(ZIP)).Returns(zipCode);

            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object, null);
            geoService.GetZipInfo("80200");


            //Act - zadzialaj i wykonaj ta metode
            var returnedZipCode = geoService.GetZipInfo(ZIP);

            //Assert - upewnij sie, ze dziala poprawnie
            Assert.IsTrue(returnedZipCode.City == "Gdansk" , "ZipCode should be Gdansk");
            Assert.IsTrue(returnedZipCode.State == "PR");
        }
    }
}
