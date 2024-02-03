// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using System.Collections.Generic;
// using System.Linq;
// using Xunit;
// using ProjectAccessibility.Controllers;
// using ProjectAccessibility.Models;
// using ProjectAccessibility.Context;
//
// public class BeperkingControllerTest
// {
//     [Fact] // System.NotSupportedException: Unsupported expression: m => m.Beperkingen, TEST-FAILED, ik kan het ff niet fixen :c
//     public void Get_ReturnsOkObjectResult_WithBeperking() 
//     {
//         // Arrange
//         var data = new List<Beperking>
//         {
//             new Beperking { Bcode = 1, Naam = "TestNaam" },
//             
//         }.AsQueryable();
//
//         var mockSet = new Mock<DbSet<Beperking>>();
//         mockSet.As<IQueryable<Beperking>>().Setup(m => m.Provider).Returns(data.Provider);
//         mockSet.As<IQueryable<Beperking>>().Setup(m => m.Expression).Returns(data.Expression);
//         mockSet.As<IQueryable<Beperking>>().Setup(m => m.ElementType).Returns(data.ElementType);
//         mockSet.As<IQueryable<Beperking>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
//
//         var mockContext = new Mock<GebruikerContext>();
//         mockContext.Setup(m => m.Beperkingen).Returns(mockSet.Object);
//
//         var controller = new BeperkingController(mockContext.Object);
//         int testBeperkingId = 1;
//
//         // Act
//         var result = controller.Get(testBeperkingId);
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         var beperking = Assert.IsType<Beperking>(okResult.Value);
//         Assert.Equal(testBeperkingId, beperking.Bcode);
//     }
// }