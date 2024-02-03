// using System;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Newtonsoft.Json;
// using ProjectAccessibility;
// using ProjectAccessibility.Context;
// using ProjectAccessibility.Models;
// using Xunit;
//
// namespace ProjectAccessibility.Test
// {
//     public class BeperkingControllerTest : IClassFixture<WebApplicationFactory<Startup>>
//     {
//         private readonly WebApplicationFactory<Startup> _factory;
//         private readonly HttpClient _client;
//
//         public BeperkingControllerTest(WebApplicationFactory<Startup> factory)
//         {
//             _factory = factory;
//             _client = factory.WithWebHostBuilder(builder =>
//             {
//                 builder.ConfigureServices(services =>
//                 {
//                     var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GebruikerContext>));
//
//                     services.Remove(descriptor);
//
//                     services.AddDbContext<GebruikerContext>(options =>
//                     {
//                         options.UseInMemoryDatabase("InMemoryDbForTesting");
//                     });
//                 });
//             }).CreateClient();
//         }
//
//         private StringContent GetStringContent(object obj)
//         {
//             return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
//         }
//
//         [Fact]
//         public async Task Get_ReturnsBeperking()
//         {
//             // Arrange
//             var expectedId = 1; // Zorg ervoor dat dit overeenkomt met een ID in je in-memory database
//
//             // Act
//             var response = await _client.GetAsync($"/api/Beperking/{expectedId}");
//             response.EnsureSuccessStatusCode();
//             var stringResponse = await response.Content.ReadAsStringAsync();
//             var result = JsonConvert.DeserializeObject<Beperking>(stringResponse);
//
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(expectedId, result.Bcode);
//         }
//
//         [Fact]
//         public async Task Post_CreatesNewBeperking()
//         {
//             // Arrange
//             var newBeperking = new BeperkingRequestModel { Naam = "TestNaam" };
//
//             // Act
//             var response = await _client.PostAsync("/api/Beperking/", GetStringContent(newBeperking));
//             response.EnsureSuccessStatusCode();
//             var stringResponse = await response.Content.ReadAsStringAsync();
//             var result = JsonConvert.DeserializeObject<Beperking>(stringResponse);
//
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal("TestNaam", result.Naam);
//         }
//
//         [Fact]
//         public async Task Put_UpdatesExistingBeperking()
//         {
//             // Arrange
//             var existingId = 1; // Zorg ervoor dat dit overeenkomt met een ID in je in-memory database
//             var updatedBeperking = new BeperkingRequestModel { Naam = "UpdatedNaam" };
//
//             // Act
//             var response = await _client.PutAsync($"/api/Beperking/{existingId}", GetStringContent(updatedBeperking));
//             response.EnsureSuccessStatusCode();
//             var stringResponse = await response.Content.ReadAsStringAsync();
//             var result = JsonConvert.DeserializeObject<Beperking>(stringResponse);
//
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal("UpdatedNaam", result.Naam);
//         }
//
//         [Fact]
//         public async Task Delete_RemovesBeperking()
//         {
//             // Arrange
//             var existingId = 1; // Zorg ervoor dat dit overeenkomt met een ID in je in-memory database die verwijderd kan worden
//
//             // Act
//             var response = await _client.DeleteAsync($"/api/Beperking/{existingId}");
//             Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//
//             // Assert - Optioneel: Verifieer dat het object verwijderd is
//         }
//     }
// }
