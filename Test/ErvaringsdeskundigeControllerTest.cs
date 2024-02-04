using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectAccessibility.Models.RequestModels;
using ProjectAccessibility.Models.ReturnModels;
using Xunit;

public class ErvaringdeskundigeControllerTest
{
    private GebruikerContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new GebruikerContext(options);
        return dbContext;
    }

    [Fact] // GET-methode
    public void Get_Returns_Ervaringdeskundige_By_Id()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var controller = new ErvaringdeskundigeController(dbContext);
        var testErvaringdeskundige = new Ervaringdeskundige
        {
            
            Voornaam = "TestVoornaam",
            Achternaam = "TestAchternaam",
            Email = "test@example.com",
            Wachtwoord = "testWachtwoord",
            Contactvoorkeur = "Email",
            Huisnummer = "123",
            Plaats = "TestPlaats",
            Postcode = "1234 AB",
            Straatnaam = "TestStraat",
            Telefoonnummer = "0123456789", };
        
        dbContext.Ervaringdeskundiges.Add(testErvaringdeskundige);
        dbContext.SaveChanges();

        // Act
        var result = controller.Get(testErvaringdeskundige.Gcode) as ObjectResult;

        // Assert
        Assert.Equal(200, result.StatusCode);
        var returnModel = result.Value as ErvaringdeskundigeReturnModel;
        Assert.NotNull(returnModel);
        Assert.Equal(testErvaringdeskundige.Voornaam, returnModel.Ervaringdeskundige.Voornaam);
    }

    
    
    [Fact]
    public void Post_Creates_New_Ervaringdeskundige() // POST-methode
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var controller = new ErvaringdeskundigeController(dbContext);
        var requestModel = new ErvaringdeskundigeRequestModel 
        {
            Voornaam = "New", 
            Email = "new@example.com", 
            Wachtwoord = "password", 
            Achternaam = "User", 
            Contactvoorkeur = "Email", 
            Huisnummer = "123", 
            Plaats = "Stad", 
            Postcode = "1234 AB", 
            Straatnaam = "Straatweg", 
            Telefoonnummer = "0123456789"
        };

        // Act
        var result = controller.Post(requestModel) as ObjectResult;

        // Assert
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(1, dbContext.Ervaringdeskundiges.Count());
        var ervaringdeskundige = dbContext.Ervaringdeskundiges.Single();
        Assert.Equal("New", ervaringdeskundige.Voornaam);
    }

    
[Fact] // PUT-methode
public void Put_Updates_Existing_Ervaringdeskundige()
{
    // Arrange
    var dbContext = GetInMemoryDbContext();
    var controller = new ErvaringdeskundigeController(dbContext);
    var existingErvaringdeskundige = new Ervaringdeskundige
    {
        Voornaam = "OudVoornaam",
        Achternaam = "OudAchternaam",
        Email = "oud@example.com",
        Wachtwoord = "oudWachtwoord",
        Contactvoorkeur = "Email",
        Huisnummer = "123",
        Plaats = "OudPlaats",
        Postcode = "1234 AB",
        Straatnaam = "OudStraat",
        Telefoonnummer = "0123456789",
    };
    
    dbContext.Ervaringdeskundiges.Add(existingErvaringdeskundige);
    dbContext.SaveChanges();

    var updatedErvaringdeskundigePutModel = new ErvaringsdeskundigePutModel
    {
        Voornaam = "NieuwVoornaam",
        Achternaam = "NieuwAchternaam",
        Email = "nieuw@example.com",
        Telefoonnummer = "9876543210",
        Straatnaam = "NieuwStraat",
        Postcode = "4321 BA",
        Huisnummer = "321",
        Geboortedatum = new DateTime(1980, 1, 1) // Pas deze datum aan naar een relevante waarde
    };

    // Act
    var result = controller.Put(existingErvaringdeskundige.Gcode, updatedErvaringdeskundigePutModel) as ObjectResult;

    // Assert
    Assert.Equal(200, result.StatusCode);
    var updatedErvaringdeskundige = dbContext.Ervaringdeskundiges.Find(existingErvaringdeskundige.Gcode);
    Assert.NotNull(updatedErvaringdeskundige);
    Assert.Equal("NieuwVoornaam", updatedErvaringdeskundige.Voornaam);
    Assert.Equal("NieuwAchternaam", updatedErvaringdeskundige.Achternaam);
    Assert.Equal("nieuw@example.com", updatedErvaringdeskundige.Email);
    Assert.Equal("9876543210", updatedErvaringdeskundige.Telefoonnummer);
    Assert.Equal("NieuwStraat", updatedErvaringdeskundige.Straatnaam);
    Assert.Equal("4321 BA", updatedErvaringdeskundige.Postcode);
    Assert.Equal("321", updatedErvaringdeskundige.Huisnummer);
    // Voeg extra asserts toe indien nodig, bijvoorbeeld voor Geboortedatum als je model dit ondersteunt
}

    
    [Fact] // DELETE-methode WERKT !!!
    public void Delete_Removes_Ervaringdeskundige_By_Id()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var controller = new ErvaringdeskundigeController(dbContext);
        var ervaringdeskundigeToDelete = new Ervaringdeskundige
        {
            Voornaam = "TestVoornaam",
            Achternaam = "TestAchternaam",
            Email = "test@example.com",
            Wachtwoord = "testWachtwoord",
            Contactvoorkeur = "Email",
            Huisnummer = "123",
            Plaats = "TestPlaats",
            Postcode = "1234 AB",
            Straatnaam = "TestStraat",
            Telefoonnummer = "0123456789",
        };
        dbContext.Ervaringdeskundiges.Add(ervaringdeskundigeToDelete);
        dbContext.SaveChanges();

        // Act
        var result = controller.Delete(ervaringdeskundigeToDelete.Gcode) as StatusCodeResult;

        // Assert
        Assert.Equal(204, result.StatusCode);
        var deletedErvaringdeskundige = dbContext.Ervaringdeskundiges.Find(ervaringdeskundigeToDelete.Gcode);
        Assert.Null(deletedErvaringdeskundige);
    }
}
