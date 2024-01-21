using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;
using ProjectAccessibility.Models.ReturnModels;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public ChatController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Chat/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Chat chat = _dbContext.Chats
                .Single(c => c.Ccode == id);
            
            
            return Ok(chat);
        }
        
        [HttpGet("GetChatsByUsers")]
        public IActionResult GetChatsByUsers(int senderCode, int receiverCode)
        {
            var chats = _dbContext.Chats
                .Where(c => (c.SenderGCode == senderCode && c.RecieverGCode == receiverCode) ||
                            (c.SenderGCode == receiverCode && c.RecieverGCode == senderCode))
                .ToList();

            List<ChatReturnModel> openChatsWithUsers = chats
                .Select(chat =>
                {
                    ChatReturnModel returnModel = new ChatReturnModel();
            
                    var ervaringdeskundige = _dbContext.Ervaringdeskundiges.Find(chat.SenderGCode);
                    var bedrijf = _dbContext.Bedrijven.Find(chat.SenderGCode);
                    var beheerder = _dbContext.Beheerders.Find(chat.SenderGCode);

                    if (ervaringdeskundige != null)
                    {
                        returnModel.SenderName = ervaringdeskundige.Voornaam;
                    }
                    else if (bedrijf != null)
                    {
                        returnModel.SenderName = bedrijf.Naam;
                    }
                    else if (beheerder != null)
                    {
                        returnModel.SenderName = beheerder.Voornaam;
                    }

                    returnModel.SenderGCode = chat.SenderGCode;
                    returnModel.RecieverGCode = chat.RecieverGCode;
                    returnModel.DateTime = chat.DateTime;
                    returnModel.Message = chat.Message;
                    
                    return returnModel;
                })
                .ToList();
            
            return Ok(openChatsWithUsers);
        }

        // POST: api/Chat/?
        [HttpPost]
        public IActionResult Post([FromBody] ChatRequestModel requestModel)
        {
            Chat newChat = new Chat()
            {
                SenderGCode = requestModel.SenderGCode,
                RecieverGCode = requestModel.RecieverGCode,
                DateTime = requestModel.DateTime,
                Message = requestModel.Message,
            };

            _dbContext.Chats.Add(newChat);
            _dbContext.SaveChanges();
            return StatusCode(201, newChat);
        }

        // PUT: api/Chat/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChatRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingChat = _dbContext.Chats.Find(id);

            if (existingChat == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingChat.SenderGCode = requestModel.SenderGCode;
            existingChat.RecieverGCode = requestModel.RecieverGCode;
            existingChat.DateTime = requestModel.DateTime;
            existingChat.Message = requestModel.Message;

            _dbContext.Entry(existingChat).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingChat); // 200 OK
        }

        // DELETE: api/Chat/?
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chatToDelete = _dbContext.Chats.Find(id);

            if (chatToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Chats.Remove(chatToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
