using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using ProjectAccessibility.Context;
using System.Linq;

public class GebruikerControllerTests
{
    [Fact] // tests the GET-method
    public void Get_Returns_Gebruiker_By_Id_WithRequiredFields()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GebruikerContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        using (var context = new GebruikerContext(options))
        {
            context.Gebruikers.Add(new Gebruiker { Email = "test@example.com", Wachtwoord = "Test1234" });
            context.SaveChanges();
        }
        
        using (var context = new GebruikerContext(options))
        {
            var controller = new GebruikerController(context);

            // Act
            var result = controller.Get(1); // CAN BE CHANGED !!!

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var gebruiker = Assert.IsType<Gebruiker>(okResult.Value);
            Assert.Equal("test@example.com", gebruiker.Email);
        }
    }
}