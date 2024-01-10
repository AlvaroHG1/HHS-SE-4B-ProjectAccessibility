using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class VoorkeurTypeController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public VoorkeurTypeController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/VoorkeurType/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Bcode)
        {
            
            var voorkeurTypes = _dbContext.VoorkeurTypes
                .Where(vt => vt.Otcode == Bcode).ToList();
            
            return Ok(voorkeurTypes);
        }
        
        // POST: api/VoorkeurType/?
        [HttpPost]
        public IActionResult Post([FromBody] VoorkeurTypeRequestModel requestModel)
        {
            VoorkeurType newVoorkeurType = new VoorkeurType()
            {
                Otcode = requestModel.Otcode,
                Ecode = requestModel.Ecode
            };

            _dbContext.VoorkeurTypes.Add(newVoorkeurType);
            _dbContext.SaveChanges();
            return StatusCode(201, newVoorkeurType);
        }

        // DELETE: api/VoorkeurType/5
        [HttpDelete]
        public IActionResult Delete([FromBody] VoorkeurTypeRequestModel requestModel)
        {
            VoorkeurType? voorkeurTypeToDelete = _dbContext.VoorkeurTypes
                .SingleOrDefault(vt => vt.Otcode == requestModel.Otcode && vt.Ecode == requestModel.Ecode);

            if (voorkeurTypeToDelete == null)
            {
                return NotFound();
            }

            _dbContext.VoorkeurTypes.Remove(voorkeurTypeToDelete);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}