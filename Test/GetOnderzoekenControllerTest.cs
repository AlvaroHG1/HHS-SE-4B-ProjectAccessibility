using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using Xunit;

namespace ProjectAccessibility.Test
{
    public class GetOnderzoekenControllerTest
    {
        private GebruikerContext _dbContext;
        private GetOnderzoekenController _controller;

        public GetOnderzoekenControllerTest()
        {
            // Initialiseer de DbContext met een in-memory database
            var options = new DbContextOptionsBuilder<GebruikerContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new GebruikerContext(options);
            _controller = new GetOnderzoekenController(_dbContext);

            // Zorg ervoor dat de database voor elke test leeg is
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            // Voeg testdata toe aan de in-memory database
            SeedTestData();
        }

        private void SeedTestData()
        {
            var ervaringdeskundige = new Ervaringdeskundige
            {
                // Vul alle verplichte velden in
                Email = "test@example.com",
                Wachtwoord = "testWachtwoord",
                Achternaam = "TestAchternaam",
                Contactvoorkeur = "Email",
                Huisnummer = "123",
                Plaats = "TestPlaats",
                Postcode = "1234 AB",
                Straatnaam = "TestStraat",
                Telefoonnummer = "0123456789",
                Voornaam = "TestVoornaam"
            };

            _dbContext.Ervaringdeskundiges.Add(ervaringdeskundige);
            _dbContext.SaveChanges();
        }

        [Fact] // tests if the correct type gets returned when an Ecode is being called 
        public void Get_ReturnsCorrectType_WhenCalledWithValidEcode()
        {
            // Arrange
            var ecode = _dbContext.Ervaringdeskundiges.First().Gcode; // Gebruik de Gcode van de toegevoegde ervaringdeskundige

            // Act
            var result = _controller.Get(ecode);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<List<Onderzoek>>(okResult.Value); // Zorg ervoor dat dit overeenkomt met de verwachte output
        }
    }
}
