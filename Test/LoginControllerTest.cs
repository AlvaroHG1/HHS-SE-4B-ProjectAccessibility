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
        var dbContext = new MockGebruikerContext(); // Je moet een mock van je context maken voor testdoeleinden
        var loginController = new LoginController(dbContext);

        // Voeg een testgebruiker toe aan de mockcontext
        var testGebruiker = new Gebruiker { Email = "test@example.com", Wachtwoord = "password123" };
        dbContext.Gebruikers.Add(testGebruiker);

        // Act
        var result = loginController.Get("test@example.com", "password123") as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var gebruikerResult = result.Value as Gebruiker;
        Assert.NotNull(gebruikerResult);
        Assert.Equal(testGebruiker.Email, gebruikerResult.Email);
        Assert.Equal(testGebruiker.Wachtwoord, gebruikerResult.Wachtwoord);
    }

    [Fact]
    public void Get_ReturnsNotFound_WithInvalidCredentials()
    {
        // Arrange
        var dbContext = new MockGebruikerContext(); // Je moet een mock van je context maken voor testdoeleinden
        var loginController = new LoginController(dbContext);

        // Act
        var result = loginController.Get("nonexistent@example.com", "invalidPassword") as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    // Voeg hier andere tests toe, afhankelijk van je vereisten
}

// MockGebruikerContext implementeert een mock van GebruikerContext voor testdoeleinden
public class MockGebruikerContext : GebruikerContext
{
    public MockGebruikerContext() : base(new DbContextOptionsBuilder<GebruikerContext>().UseInMemoryDatabase("MockDatabase").Options)
    {
    }
}