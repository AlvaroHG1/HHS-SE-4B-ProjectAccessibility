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
    public class VoogdController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public VoogdController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Voogd/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Voogd voogd = _dbContext.Voogden
                .Single(c => c.Vcode == id);
            
            
            return Ok(voogd);
        }

        // POST: api/Voogd/?
        [HttpPost]
        public IActionResult Post([FromBody] VoogdRequestModel requestModel)
        {
            Voogd newVoogd = new Voogd()
            {
                Voornaam = requestModel.Voornaam,
                Achternaam = requestModel.Achternaam,
                Email = requestModel.Email,
                Telefoonnummer = requestModel.Telefoonnummer,
                Postcode = requestModel.Postcode
            };
            _dbContext.Voogden.Add(newVoogd);
            _dbContext.SaveChanges();
            

            HeeftVoogd newHeeftVoogd = new HeeftVoogd()
            {
                Ecode = requestModel.ErvaringdeskundigeGCoded,
                Vcode = newVoogd.Vcode
            };

            _dbContext.HeeftVoogden.Add(newHeeftVoogd);
            _dbContext.SaveChanges();
                
            return StatusCode(201, newVoogd);
        }

        // PUT: api/Voogd/?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VoogdRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }
            
            var existingVoogd = _dbContext.Voogden.Find(id);

            if (existingVoogd == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingVoogd.Voornaam = requestModel.Voornaam;
            existingVoogd.Achternaam = requestModel.Achternaam;
            existingVoogd.Email = requestModel.Email;
            existingVoogd.Telefoonnummer = requestModel.Telefoonnummer;
            existingVoogd.Postcode = requestModel.Postcode;

            _dbContext.Entry(existingVoogd).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingVoogd); // 200 OK
        }

        // DELETE: api/Voogd/?
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var voogdToDelete = _dbContext.Voogden.Find(id);

            if (voogdToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Voogden.Remove(voogdToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
