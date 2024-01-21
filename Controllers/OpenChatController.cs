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
    public class OpenChatController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public OpenChatController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var openChatCodes = _dbContext.OpenChats
                .Where(oc => oc.SenderGCode == id)
                .Select(oc => new { oc.RecieverGCode, oc.SenderGCode })
                .ToList();

            List<OpenChatReturnModel> openChatsWithUsers = openChatCodes
                .Select(chat =>
                {
                    OpenChatReturnModel returnModel = new OpenChatReturnModel();
            
                    var ervaringdeskundige = _dbContext.Ervaringdeskundiges.Find(chat.RecieverGCode);
                    var bedrijf = _dbContext.Bedrijven.Find(chat.RecieverGCode);
                    var beheerder = _dbContext.Beheerders.Find(chat.RecieverGCode);

                    if (ervaringdeskundige != null)
                    {
                        returnModel.Naam = ervaringdeskundige.Voornaam;
                        returnModel.Type = "Ervaringsdeskundige";
                    }
                    else if (bedrijf != null)
                    {
                        returnModel.Naam = bedrijf.Naam;
                        returnModel.Type = "Bedrijf";
                    }
                    else if (beheerder != null)
                    {
                        returnModel.Naam = beheerder.Voornaam;
                        returnModel.Type = "Beheerder";
                    }

                    returnModel.SenderGCode = chat.SenderGCode;
                    returnModel.ReceiverGCode = chat.RecieverGCode;

                    return returnModel;
                })
                .ToList();
    
            return Ok(openChatsWithUsers);
        }

        // POST: api/Chat/?
        [HttpPost]
        public IActionResult Post([FromBody] OpenChatRequestModel requestModel)
        {

            int recieverGCode = requestModel.SecondGCode;
            int senderGCode = requestModel.FirstGCode;
            
            if (_dbContext.OpenChats.SingleOrDefault(oc => oc.RecieverGCode == recieverGCode && oc.SenderGCode == senderGCode) != null)
            {
                return BadRequest();
            }
            
            OpenChat newSenderOpenChat = new OpenChat()
            {
                SenderGCode = requestModel.FirstGCode,
                RecieverGCode = requestModel.SecondGCode
            };
            
            OpenChat newRecieverOpenChat = new OpenChat()
            {
                SenderGCode = requestModel.SecondGCode,
                RecieverGCode = requestModel.FirstGCode
            };

            _dbContext.OpenChats.Add(newSenderOpenChat);
            _dbContext.SaveChanges();
            _dbContext.OpenChats.Add(newRecieverOpenChat);
            _dbContext.SaveChanges();
            return StatusCode(201);
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