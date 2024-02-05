using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using System.Collections.Generic;

public class HeeftVoogdControllerTest
{
    private readonly Mock<GebruikerContext> _mockContext;
    private readonly HeeftVoogdController _controller;

    public HeeftVoogdControllerTest()
    {
        _mockContext = new Mock<GebruikerContext>(new DbContextOptions<GebruikerContext>());
        _controller = new HeeftVoogdController(_mockContext.Object);
    }

    
    //tests whether the GET-method in HeeftVoogdController.cs functions correctly by returning the expected data based a given Ecode
    [Fact]
    public void Get_ReturnsCorrectData()
    {
        // Arrange
        var ecodeTest = 1;
        var heeftVoogdenData = new List<HeeftVoogd>
        {
            new HeeftVoogd { Ecode = ecodeTest, Vcode = 1 },
            new HeeftVoogd { Ecode = ecodeTest, Vcode = 2 }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HeeftVoogd>>();
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.Provider).Returns(heeftVoogdenData.Provider);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.Expression).Returns(heeftVoogdenData.Expression);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.ElementType).Returns(heeftVoogdenData.ElementType);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.GetEnumerator()).Returns(heeftVoogdenData.GetEnumerator());

        _mockContext.Setup(c => c.HeeftVoogden).Returns(mockSet.Object);

        // Act
        var result = _controller.Get(ecodeTest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<HeeftVoogd>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    
    // tests wether the DELETE-method in HeeftVoogdController.cs successfully removes a HeeftVoogd entity from the mock db context 
    [Fact]
    public void Delete_RemovesData()
    {
        // Arrange
        var requestModel = new HeeftVoogdRequestModel { Ecode = 1, Vcode = 1 };
        var heeftVoogdData = new List<HeeftVoogd> 
        { 
            new HeeftVoogd { Ecode = 1, Vcode = 1 } 
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HeeftVoogd>>();
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.Provider).Returns(heeftVoogdData.Provider);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.Expression).Returns(heeftVoogdData.Expression);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.ElementType).Returns(heeftVoogdData.ElementType);
        mockSet.As<IQueryable<HeeftVoogd>>().Setup(m => m.GetEnumerator()).Returns(heeftVoogdData.GetEnumerator());

        mockSet.Setup(m => m.Remove(It.IsAny<HeeftVoogd>())).Callback<HeeftVoogd>((entity) => heeftVoogdData.ToList().Remove(entity));

        _mockContext.Setup(c => c.HeeftVoogden).Returns(mockSet.Object);

        // Act
        var result = _controller.Delete(requestModel);

        // Assert
        mockSet.Verify(m => m.Remove(It.IsAny<HeeftVoogd>()), Times.Once());
        _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        Assert.IsType<NoContentResult>(result);
    }
}
