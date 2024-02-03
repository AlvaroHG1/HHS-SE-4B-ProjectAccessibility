namespace ProjectAccessibility.Controllers.Tests;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
public class ApiTests
{
    
    [Fact]
    public async Task GetOnderzoekenTask()
    {
        // Arrange 
        using (var client = new HttpClient())
        {
            var apiUrl = "https://localhost:7216/api/GetOnderzoeken/3";

            // Act
            var response = await client.GetAsync(apiUrl);

            // Assert
            Assert.True(response != null);
        }
    }
}