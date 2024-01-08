using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftHulpmiddelController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftHulpmiddelController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftHulpmiddel/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Ecode)
        {

            var heeftHulpmiddellen = _dbContext.HeeftHulpmiddelen
                .Where(hh => hh.Ecode == Ecode).ToList();
            
            return Ok(heeftHulpmiddellen);
        }

        [HttpPost]
        public IActionResult Post([FromBody] HeeftHulpmiddelRequestModel requestModel)
        {
            HeeftHulpmiddel newHeeftHulpmiddel = new HeeftHulpmiddel()
            {
                Hcode = requestModel.Hcode,
                Ecode = requestModel.Ecode
            };

            _dbContext.HeeftHulpmiddelen.Add(newHeeftHulpmiddel);
            _dbContext.SaveChanges();
            return StatusCode(201, newHeeftHulpmiddel);
        }

        // DELETE: api/HeeftHulpmiddel/?
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftHulpmiddelRequestModel requestModel)
        {
            HeeftHulpmiddel? heeftHulpmiddelToDelete = _dbContext.HeeftHulpmiddelen
                .SingleOrDefault(hh => hh.Ecode == requestModel.Ecode && hh.Hcode == requestModel.Hcode);

            if (heeftHulpmiddelToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.HeeftHulpmiddelen.Remove(heeftHulpmiddelToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}