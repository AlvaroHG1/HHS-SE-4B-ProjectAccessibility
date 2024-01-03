using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeheerderController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public BeheerderController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Beheerder/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Beheerder beheerder = _dbContext.Beheerders
                .Single(b => b.Gcode == id);
            
            
            return Ok(beheerder);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BeheerderRequestModel requestModel)
        {
            if (requestModel == null || string.IsNullOrEmpty(requestModel.Email) || string.IsNullOrEmpty(requestModel.Wachtwoord))
            {
                return BadRequest("Email and Wachtwoord are required");
            }

            Beheerder newBeheerder = new Beheerder()
            {
                Voornaam = requestModel.Voornaam,
                Achternaam = requestModel.Achternaam,
                Rol = requestModel.Rol,
                Email = requestModel.Email,
                Wachtwoord = requestModel.Wachtwoord
            };

            _dbContext.Beheerders.Add(newBeheerder);
            _dbContext.SaveChanges();
            return StatusCode(201, newBeheerder);
        }

        // PUT: api/Gebruiker/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BeheerderRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }

            // Find the existing Gebruiker with the given ID
            var existingBeheerder = _dbContext.Beheerders.Find(id);

            if (existingBeheerder == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingBeheerder.Email = requestModel.Email;
            existingBeheerder.Wachtwoord = requestModel.Wachtwoord;
            existingBeheerder.Voornaam = requestModel.Voornaam;
            existingBeheerder.Achternaam = requestModel.Achternaam;
            existingBeheerder.Rol = requestModel.Rol;

            _dbContext.Entry(existingBeheerder).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingBeheerder); // 200 OK
        }

        // DELETE: api/Gebruiker/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var beheerderToDelete = _dbContext.Beheerders.Find(id);

            if (beheerderToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Beheerders.Remove(beheerderToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}