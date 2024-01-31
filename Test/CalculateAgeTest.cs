using System;
using Xunit;
using ProjectAccessibility;
using ProjectAccessibility.Controllers;

namespace ProjectAccessibility.Tests
{
    public class GetOnderzoekenControllerTests
    {
        [Fact]
        public void CalculateAge_ReturnsCorrectAge()
        {
            // Arrange
            var onderzoekenController = new GetOnderzoekenController(null);
            var birthDate = new DateTime(1990, 1, 1);

            // Act
            var result = onderzoekenController.CalculateAge(birthDate);

            // Assert
            var today = DateTime.Today;
            var expectedAge = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-expectedAge))
                expectedAge--;

            Assert.Equal(expectedAge, result);
        }

        [Fact] // this method tests the calculation for when 0 is being types from the users keyboard as their birthday
        public void CalculateAge_ReturnsZeroForFutureBirthDate()
        {
            // Arrange
            var onderzoekenController = new GetOnderzoekenController(null);
            var futureBirthDate = DateTime.Today.AddYears(1);

            // Act
            var result = onderzoekenController.CalculateAge(futureBirthDate);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact] 
        public void CalculateAge_ReturnsZeroForTodayBirthDate()
        {
            // Arrange
            var onderzoekenController = new GetOnderzoekenController(null);
            var todayBirthDate = DateTime.Today;

            // Act
            var result = onderzoekenController.CalculateAge(todayBirthDate);

            // Assert
            Assert.Equal(0, result);
        }
    }
}