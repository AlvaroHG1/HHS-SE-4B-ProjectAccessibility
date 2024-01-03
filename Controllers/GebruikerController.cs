using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public GebruikerController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Gebruiker/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Gebruiker gebruiker = _dbContext.Gebruikers
                .Single(g => g.Gcode == id);
            
            return Ok(gebruiker);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GebruikerRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            Gebruiker newGebruiker = new Gebruiker
            {
                Email = requestModel.Email,
                Wachtwoord = requestModel.Wachtwoord,
                UserType = requestModel.UserType
            };

            _dbContext.Gebruikers.Add(newGebruiker);
            _dbContext.SaveChanges();
            
            return StatusCode(201, newGebruiker);
        }

        // PUT: api/Gebruiker/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GebruikerRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }

            // Find the existing Gebruiker with the given ID
            var existingGebruiker = _dbContext.Gebruikers.Find(id);

            if (existingGebruiker == null)
            {
                return NotFound(); // 404 Not Found
            }
            
            existingGebruiker.Email = requestModel.Email;
            existingGebruiker.Wachtwoord = requestModel.Wachtwoord;
            existingGebruiker.UserType = requestModel.UserType;
            
            _dbContext.Entry(existingGebruiker).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingGebruiker); // 200 OK
        }
        
        // DELETE: api/Gebruiker/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gebruikerToDelete = _dbContext.Gebruikers.Find(id);

            if (gebruikerToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }
            
            _dbContext.Gebruikers.Remove(gebruikerToDelete);
            
            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}