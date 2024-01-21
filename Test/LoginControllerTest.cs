using Microsoft.AspNetCore.Mvc;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class LoginControllerTests
{
    [Fact]
    public void Get_ReturnsOkObjectResult_WithValidCredentials()
    {
        // Arrange
        var dbContext = new MockGebruikerContext();
        var loginController = new LoginController(dbContext);

        // Add a test user to the mock context
        var testGebruiker = new Gebruiker { Gcode = 1, Email = "test@example.com", Wachtwoord = "password123" };
        dbContext.Gebruikers.Add(testGebruiker);

        // Act
        var result = loginController.Get("test@example.com", "password123") as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        // Check the type of the returned value
        Assert.IsType<ActionResult<Gebruiker>>(result);

        // Check properties of the returned Gebruiker object
        var gebruikerResult = result.Value as Gebruiker;
        Assert.NotNull(gebruikerResult);
        Assert.Equal(testGebruiker.Gcode, gebruikerResult.Gcode);
        Assert.Equal(testGebruiker.Email, gebruikerResult.Email);
        Assert.Equal(testGebruiker.Wachtwoord, gebruikerResult.Wachtwoord);
    }

    [Fact]
    public void Get_ReturnsNotFound_WithInvalidCredentials()
    {
        // Arrange
        var dbContext = new MockGebruikerContext();
        var loginController = new LoginController(dbContext);

        // Act
        var result = loginController.Get("nonexistent@example.com", "invalidPassword") as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }
    
}

// MockGebruikerContext implementeert een mock van GebruikerContext voor testdoeleinden
public class MockGebruikerContext : GebruikerContext
{
    public MockGebruikerContext() : base(new DbContextOptionsBuilder<GebruikerContext>().UseInMemoryDatabase("MockDatabase").Options)
    {
    }
}