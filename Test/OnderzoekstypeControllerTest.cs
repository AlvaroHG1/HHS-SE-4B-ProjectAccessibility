using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using System.Linq;

public class OnderzoekstypeControllerTest
{
    private readonly Mock<GebruikerContext> _mockContext;
    private readonly OnderzoekstypeController _controller;
    private readonly List<Onderzoekstype> _onderzoekstypes;

    public OnderzoekstypeControllerTest()
    {
        _mockContext = new Mock<GebruikerContext>(new DbContextOptions<GebruikerContext>());
        _controller = new OnderzoekstypeController(_mockContext.Object);
        _onderzoekstypes = new List<Onderzoekstype>
        {
            new Onderzoekstype { Otcode = 1, Type = "Type1" },
            new Onderzoekstype { Otcode = 2, Type = "Type2" }
        };

        var mockSet = CreateMockSet(_onderzoekstypes.AsQueryable());

        _mockContext.Setup(c => c.Onderzoekstypes).Returns(mockSet.Object);
    }

    private Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }
    
    // the GET-method tests if OnderzoeksController correctly retrieves the correct Onderzoekstype
    [Fact]
    public void Get_ReturnsCorrectOnderzoekstype()
    {
        // Arrange
        var otcode = 1;

        // Act
        var result = _controller.Get(otcode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var onderzoekstype = Assert.IsType<Onderzoekstype>(okResult.Value);
        Assert.Equal(otcode, onderzoekstype.Otcode);
    }

    
    // tests wether the POST-method coorectly adds an Onderzoekype to the db
    // if everything works planned out, the response will be 201 confirming the entity was created
    [Fact] 
    public void Post_AddsNewOnderzoekstype()
    {
        // Arrange
        var requestModel = new OnderzoekstypeRequestModel { Type = "Type3" };

        // Act
        var result = _controller.Post(requestModel);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, statusCodeResult.StatusCode);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once());
    }

    
    // tests if the DELETE-method correctly removes an Onderzoekstype from the db
    [Fact]
    public void Delete_RemovesOnderzoekstype()
    {
        // Arrange
        var otcodeToDelete = 1;
        _mockContext.Setup(c => c.Onderzoekstypes.Find(otcodeToDelete)).Returns(_onderzoekstypes.FirstOrDefault(x => x.Otcode == otcodeToDelete));

        // Act
        var result = _controller.Delete(otcodeToDelete);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once());
    }
}
