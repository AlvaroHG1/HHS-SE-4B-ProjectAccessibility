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
    public class HulpmiddelController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HulpmiddelController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Hulpmiddel/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Hulpmiddel hulpmiddellen = _dbContext.Hulpmiddelen
                .Single(h => h.Hcode == id);
            
            
            return Ok(hulpmiddellen);
        }

        // POST: api/Hulpmiddel/?
        [HttpPost]
        public IActionResult Post([FromBody] HulpmiddelRequestModel requestModel)
        {
            Hulpmiddel newHulpmiddel = new Hulpmiddel()
            {
                Naam = requestModel.Naam
            };

            _dbContext.Hulpmiddelen.Add(newHulpmiddel);
            _dbContext.SaveChanges();
            return StatusCode(201, newHulpmiddel);
        }

        // PUT: api/Hulpmiddelen/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HulpmiddelRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingHulpmiddel = _dbContext.Hulpmiddelen.Find(id);

            if (existingHulpmiddel == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingHulpmiddel.Naam = requestModel.Naam;

            _dbContext.Entry(existingHulpmiddel).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingHulpmiddel); // 200 OK
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
