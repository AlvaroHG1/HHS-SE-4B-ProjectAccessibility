using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Models;
using System;
using System.Collections.Generic;
using ProjectAccessibility.Models.ReturnModels;
using System.Linq;
using Xunit;

public class ChatControllerTest
{
    private GebruikerContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // makes sure every (new) chat gets a new InMemoryDatabase
            .Options;
        var dbContext = new GebruikerContext(options);
        return dbContext;
    }

    [Fact] // tests if the GET-method succesfully gets a chat based on the ID
    public void Get_Returns_Chat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var chatController = new ChatController(dbContext);
        var expectedChat = new Chat { Ccode = 1, Message = "Test Message" };
        dbContext.Chats.Add(expectedChat);
        dbContext.SaveChanges();

        // Act
        var result = chatController.Get(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var chat = result.Value as Chat;
        Assert.NotNull(chat);
        Assert.Equal("Test Message", chat.Message);
    }

    [Fact] // tests if the GetChatsByUsers method works correctly between 2 different senders
    public void GetChatsByUsers_Returns_Chats()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var chatController = new ChatController(dbContext);
        
        dbContext.Chats.AddRange(
            new Chat { SenderGCode = 1, RecieverGCode = 2, Message = "Hello" },
            new Chat { SenderGCode = 2, RecieverGCode = 1, Message = "Hi" }
        );
        dbContext.SaveChanges();

        // Act
        var result = chatController.GetChatsByUsers(1, 2) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var chats = result.Value as List<ChatReturnModel>;
        Assert.NotNull(chats);
        Assert.Equal(2, chats.Count);
    }

    [Fact] // tests if the POST-method can succesfully make a new chat and save it in the db
    public void Post_Creates_New_Chat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var chatController = new ChatController(dbContext);
        var newChat = new ChatRequestModel { SenderGCode = 1, RecieverGCode = 2, Message = "Test" };

        // Act
        var result = chatController.Post(newChat) as ObjectResult;

        // Assert
        Assert.Equal(201, result.StatusCode);
        Assert.Single(dbContext.Chats);
    }

    [Fact] // tests if the PUT-method works by updating an already existing msg in the db
    public void Put_Updates_Chat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var chatController = new ChatController(dbContext);
        var originalChat = new Chat { Ccode = 1, SenderGCode = 1, RecieverGCode = 2, Message = "Old Message" };
        dbContext.Chats.Add(originalChat);
        dbContext.SaveChanges();
        var updatedChat = new ChatRequestModel { SenderGCode = 1, RecieverGCode = 2, Message = "Updated Message" };

        // Act
        var result = chatController.Put(1, updatedChat) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var chat = result.Value as Chat;
        Assert.NotNull(chat);
        Assert.Equal("Updated Message", chat.Message);
    }

    [Fact] // tests if the DELETE-method successfully removes a chat msg from the db
    public void Delete_Removes_Chat()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var chatController = new ChatController(dbContext);
        var chatToDelete = new Chat { Ccode = 1, Message = "Delete me" };
        dbContext.Chats.Add(chatToDelete);
        dbContext.SaveChanges();

        // Act
        var result = chatController.Delete(1) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain(dbContext.Chats, c => c.Ccode == 1);
    }
}
