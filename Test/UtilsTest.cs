using Xunit;
using ProjectAccessibility.Controllers;

namespace ProjectAccessibility.Test
{
    // dit is een security test :D !!!
    public class UtilsTests
    {
        [Fact]
        public void HashPassword_ReturnsNonNullString()
        {
            // Arrange
            string password = "myPassword";

            // Act
            string hashedPassword = Utils.HashPassword(password);

            // Assert
            Assert.NotNull(hashedPassword);
        }

        [Fact]
        public void PasswordMatch_ReturnsTrueForCorrectPassword()
        {
            // Arrange
            string password = "myPassword";
            string hashedPassword = Utils.HashPassword(password);

            // Act
            bool isMatch = Utils.PasswordMatch(password, hashedPassword);

            // Assert
            Assert.True(isMatch);
        }

        [Fact]
        public void PasswordMatch_ReturnsFalseForIncorrectPassword()
        {
            // Arrange
            string password = "myPassword";
            string hashedPassword = Utils.HashPassword(password);
            string wrongPassword = "wrongPassword";

            // Act
            bool isMatch = Utils.PasswordMatch(wrongPassword, hashedPassword);

            // Assert
            Assert.False(isMatch);
        }
    }
}