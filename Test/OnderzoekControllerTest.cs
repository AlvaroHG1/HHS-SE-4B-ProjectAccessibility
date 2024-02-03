using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using Xunit;

namespace ProjectAccessibility.Tests.OnderzoekControllerTest    
{
    public class OnderzoekControllerTest
    {
        private readonly GebruikerContext _dbContext;

        public OnderzoekControllerTest()
        {
            var options = new DbContextOptionsBuilder<GebruikerContext>()
                .UseInMemoryDatabase(databaseName: "Gebruiker")
                .Options;
            _dbContext = new GebruikerContext(options);
        }

        [Fact]
        public void Put_Returns_OkResult()
        {
            // Arrange
            var controller = new OnderzoekController(_dbContext);
            var onderzoek = new Onderzoek
            {
                Ocode = 1,
                Titel = "test",
                Beschrijving = "test",
                Locatie = "test",
                Startdatum = DateOnly.FromDateTime(DateTime.Now),
                Einddatum = DateOnly.FromDateTime(DateTime.Now),
                GezochteBeperking = "test",
                GezochtePostcode = 12,
                MinLeeftijd = 1,
                MaxLeeftijd = 1
            };
            _dbContext.Onderzoeken.Add(onderzoek);
            _dbContext.SaveChanges();

            var requestModel = new OnderzoekRequestModel
            {
                Titel = "updated title",
                Beschrijving = "updated description",
                Locatie = "updated location",
                Startdatum = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Einddatum = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                GezochteBeperking = "updated limitation",
                GezochtePostcode = 1234,
                MinLeeftijd = 18,
                MaxLeeftijd = 99
            };

            // Act
            var result = controller.Put(onderzoek.Ocode, requestModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            
            var okResult = result as OkObjectResult;
            var updatedOnderzoek = okResult?.Value as Onderzoek;
            Assert.NotNull(updatedOnderzoek);
            Assert.Equal(requestModel.Titel, updatedOnderzoek.Titel);
            Assert.Equal(requestModel.Beschrijving, updatedOnderzoek.Beschrijving);
            Assert.Equal(requestModel.Locatie, updatedOnderzoek.Locatie);
            Assert.Equal(requestModel.Startdatum, updatedOnderzoek.Startdatum);
            Assert.Equal(requestModel.Einddatum, updatedOnderzoek.Einddatum);
            Assert.Equal(requestModel.GezochteBeperking, updatedOnderzoek.GezochteBeperking);
            Assert.Equal(requestModel.GezochtePostcode, updatedOnderzoek.GezochtePostcode);
            Assert.Equal(requestModel.MinLeeftijd, updatedOnderzoek.MinLeeftijd);
            Assert.Equal(requestModel.MaxLeeftijd, updatedOnderzoek.MaxLeeftijd);
        }
        [Fact]
        public void Delete_Returns_NoContentResult_When_OnderzoekExists()
        {
            // Arrange
            var controller = new OnderzoekController(_dbContext);
            var onderzoek = new Onderzoek
            {
                Ocode = 1,
                Titel = "test",
                Beschrijving = "test",
                Locatie = "test",
                Startdatum = DateOnly.FromDateTime(DateTime.Now),
                Einddatum = DateOnly.FromDateTime(DateTime.Now),
                GezochteBeperking = "test",
                GezochtePostcode = 12,
                MinLeeftijd = 1,
                MaxLeeftijd = 1
            };
            _dbContext.Onderzoeken.Add(onderzoek);
            _dbContext.SaveChanges();

            // Act
            var result = controller.Delete(onderzoek.Ocode);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_Returns_NotFoundResult_When_OnderzoekDoesNotExist()
        {
            // Arrange
            var controller = new OnderzoekController(_dbContext);
            var nonExistingOnderzoekId = 999; // assuming this ID doesn't exist

            // Act
            var result = controller.Delete(nonExistingOnderzoekId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
