using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AandoeningController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public AandoeningController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Aandoening/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Aandoening aandoening = _dbContext.Aandoeningen
                .Single(a => a.Acode == id);
            
            
            return Ok(aandoening);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AandoeningRequestModel requestModel)
        {
            Aandoening newAandoening = new Aandoening()
            {
                Naam = requestModel.Naam
            };

            _dbContext.Aandoeningen.Add(newAandoening);
            _dbContext.SaveChanges();
            return StatusCode(201, newAandoening);
        }

        // PUT: api/Aandoening/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AandoeningRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingAandoening = _dbContext.Aandoeningen.Find(id);

            if (existingAandoening == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingAandoening.Naam = requestModel.Naam;

            _dbContext.Entry(existingAandoening).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingAandoening); // 200 OK
        }

        // DELETE: api/Gebruiker/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aandoeningToDelete = _dbContext.Aandoeningen.Find(id);

            if (aandoeningToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Aandoeningen.Remove(aandoeningToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}