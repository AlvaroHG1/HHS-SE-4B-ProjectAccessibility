using System;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using ProjectAccessibility.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProjectAccessibility.Models.ReturnModels;

namespace ProjectAccessibility.Test
{
    public class BedrijfControllerTest
    {
        
        // Tests getting the ID of a bedrijf
        [Fact]
        public void Get_ReturnsCorrectBedrijf()
        {
            // Arrange
            var bedrijfId = 1; 
            var bedrijf = new Bedrijf { Gcode = bedrijfId, Naam = "TestBedrijf" };

            var bedrijven = new List<Bedrijf>
            {
                bedrijf
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Bedrijf>>();
            mockSet.As<IQueryable<Bedrijf>>().Setup(m => m.Provider).Returns(bedrijven.Provider);
            mockSet.As<IQueryable<Bedrijf>>().Setup(m => m.Expression).Returns(bedrijven.Expression);
            mockSet.As<IQueryable<Bedrijf>>().Setup(m => m.ElementType).Returns(bedrijven.ElementType);
            mockSet.As<IQueryable<Bedrijf>>().Setup(m => m.GetEnumerator()).Returns(bedrijven.GetEnumerator());

            var mockContext = new Mock<GebruikerContext>();
            mockContext.Setup(c => c.Bedrijven).Returns(mockSet.Object);

            var controller = new BedrijfController(mockContext.Object);

            // Act
            var result = controller.Get(bedrijfId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var returnModel = result.Value as BedrijfReturnModel;
            Assert.NotNull(returnModel);
            Assert.Equal(bedrijf.Naam, returnModel.Bedrijf.Naam);
        }
    }
}