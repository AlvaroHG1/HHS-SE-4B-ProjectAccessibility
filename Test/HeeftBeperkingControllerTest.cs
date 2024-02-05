using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using Xunit;

namespace ProjectAccessibility.Tests.HeeftBeperkingControllerTest
{
    public class HeeftBeperkingControllerTest
    {
        private readonly GebruikerContext _dbContext;

        public HeeftBeperkingControllerTest()
        {
            var options = new DbContextOptionsBuilder<GebruikerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new GebruikerContext(options);
        }

        [Fact]
        public void Delete_Returns_NoContentResult_When_HeeftBeperkingExists()
        {
            // Arrange
            var controller = new HeeftBeperkingController(_dbContext);
            var heeftBeperking = new HeeftBeperking
            {
                Bcode = 1,
                Ecode = 1
            };
            _dbContext.HeeftBeperkingen.Add(heeftBeperking);
            _dbContext.SaveChanges();

            // Act
            var result = controller.Delete(new HeeftBeperkingRequestModel { Bcode = 1, Ecode = 1 });

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_Returns_NotFoundResult_When_HeeftBeperkingDoesNotExist()
        {
            // Arrange
            var controller = new HeeftBeperkingController(_dbContext);
            var nonExistingHeeftBeperking = new HeeftBeperkingRequestModel { Bcode = 999, Ecode = 999 };

            // Act
            var result = controller.Delete(nonExistingHeeftBeperking);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
