using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenChatController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public OpenChatController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/OpenChat/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            List<OpenChat> openChats = _dbContext.OpenChats
                .Where(oc => oc.RecieverGCode == id || oc.SenderGCode == id)
                .ToList();
            
            
            return Ok(openChats);
        }

        // POST: api/Chat/?
        [HttpPost]
        public IActionResult Post([FromBody] OpenChatRequestModel requestModel)
        {

            int recieverGCode = requestModel.RecieverGCode;
            int senderGCode = requestModel.SenderGCode;
            
            if (_dbContext.OpenChats.SingleOrDefault(oc => oc.RecieverGCode == recieverGCode && oc.SenderGCode == senderGCode) != null)
            {
                return BadRequest();
            }
            
            OpenChat newOpenChat = new OpenChat()
            {
                SenderGCode = requestModel.SenderGCode,
                RecieverGCode = requestModel.RecieverGCode
            };

            _dbContext.OpenChats.Add(newOpenChat);
            _dbContext.SaveChanges();
            return StatusCode(201, newOpenChat);
        }
        
        // PUT: api/Chat/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OpenChatRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingOpenChat = _dbContext.OpenChats.Find(id);

            if (existingOpenChat == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingOpenChat.SenderGCode = requestModel.SenderGCode;
            existingOpenChat.RecieverGCode = requestModel.RecieverGCode;

            _dbContext.Entry(existingOpenChat).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingOpenChat); // 200 OK
        }

        // DELETE: api/Chat/?
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var openChatToDelete = _dbContext.OpenChats.Find(id);

            if (openChatToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.OpenChats.Remove(openChatToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}