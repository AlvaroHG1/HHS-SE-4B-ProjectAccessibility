using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: api/Beheerder/?
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
            Beheerder newBeheerder = new Beheerder()
            {
                Voornaam = requestModel.Voornaam,
                Achternaam = requestModel.Achternaam,
                Rol = requestModel.Rol,
                Email = requestModel.Email,
                Wachtwoord = Utils.HashPassword(requestModel.Wachtwoord)
            };

            _dbContext.Beheerders.Add(newBeheerder);
            _dbContext.SaveChanges();
            return StatusCode(201, newBeheerder);
        }

        // PUT: api/Beheerder/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BeheerderRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingBeheerder = _dbContext.Beheerders.Find(id);

            if (existingBeheerder == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingBeheerder.Email = requestModel.Email;
            existingBeheerder.Wachtwoord = requestModel.Wachtwoord;
            existingBeheerder.Voornaam = requestModel.Voornaam;
            existingBeheerder.Achternaam = requestModel.Achternaam;
            existingBeheerder.Rol = Utils.HashPassword(requestModel.Rol);

            _dbContext.Entry(existingBeheerder).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingBeheerder); // 200 OK
        }

        // DELETE: api/Beheerder/?
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