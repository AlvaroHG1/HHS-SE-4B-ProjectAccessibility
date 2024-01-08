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
    public class BeperkingController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public BeperkingController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Beperking/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Beperking beperking = _dbContext.Beperkingen
                .Single(b => b.Bcode == id);
            
            
            return Ok(beperking);
        }

        // POST: api/Beperking/?
        [HttpPost]
        public IActionResult Post([FromBody] BeperkingRequestModel requestModel)
        {
            Beperking newBeperking = new Beperking()
            {
                Naam = requestModel.Naam
            };

            _dbContext.Beperkingen.Add(newBeperking);
            _dbContext.SaveChanges();
            return StatusCode(201, newBeperking);
        }

        // PUT: api/Beperking/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BeperkingRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingBeperking = _dbContext.Beperkingen.Find(id);

            if (existingBeperking == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingBeperking.Naam = requestModel.Naam;

            _dbContext.Entry(existingBeperking).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingBeperking); // 200 OK
        }

        // DELETE: api/Hulpmiddel/?
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hulpmiddelToDelete = _dbContext.Hulpmiddelen.Find(id);

            if (hulpmiddelToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Hulpmiddelen.Remove(hulpmiddelToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
