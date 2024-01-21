using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using Xunit;

namespace ProjectAccessibility.Test;

public class AandoeningControllerTest
{

  [Fact]
  public void GetAandoeningByIdTest()
  {
    // Arrange
    var options = new DbContextOptionsBuilder<GebruikerContext>()
      .UseInMemoryDatabase(databaseName: "aandoeningen")
      .Options;
    // Act
    using (var context = new GebruikerContext(options))
    {
      context.Aandoeningen.Add(new Aandoening()
      {
        Acode = 1,
        Naam = "astma"
      });
      context.SaveChanges();
    }

    // Assert
    using (var context = new GebruikerContext(options))
    {
      var controller = new AandoeningController(context);
      var result = controller.Get(1);
      var okResult = result as OkObjectResult;
      var aandoening = okResult.Value as Aandoening;
      Assert.Equal(1, aandoening.Acode);
      Assert.Equal("astma", aandoening.Naam);
    }
  }

  [Fact]
  public void PostAandoeningTest()
  {
    // Arrange
    var options = new DbContextOptionsBuilder<GebruikerContext>()
      .UseInMemoryDatabase(databaseName: "aandoeningen")
      .Options;

    // Act
    using (var context = new GebruikerContext(options))
    {
      context.Aandoeningen.Add(new Aandoening()
      {
        Acode = 4,
        Naam = "astma"
      });
      context.SaveChanges();
    }

    // Assert
    using (var context = new GebruikerContext(options))
    {
      var controller = new AandoeningController(context);
      var result = controller.Post(new AandoeningRequestModel()
      {
        Naam = "astma"
      });
      var okResult = result as StatusCodeResult;
      Assert.Equal(201, okResult.StatusCode);

    }
  }

  [Fact]
  public void DeleteAandoeningTest()
  {
    // Arrange
    var options = new DbContextOptionsBuilder<GebruikerContext>()
      .UseInMemoryDatabase(databaseName: "aandoeningen")
      .Options;
    // Act
    using (var context = new GebruikerContext(options))
    {
      context.Aandoeningen.Add(new Aandoening()
      {
        Acode = 1,
        Naam = "astma"
      });
      context.SaveChanges();
    }

    // Assert
    using (var context = new GebruikerContext(options))
    {
      var controller = new AandoeningController(context);
      //   var result = controller.Delete(1);
    //  var okResult = result as StatusCodeResult;
     // Assert.Equal(204, okResult.StatusCode);

      // Controleer of de aandoening daadwerkelijk is verwijderd
      var deletedAandoening = context.Aandoeningen.Find(1);
      Assert.Null(deletedAandoening);
    }
  }
}