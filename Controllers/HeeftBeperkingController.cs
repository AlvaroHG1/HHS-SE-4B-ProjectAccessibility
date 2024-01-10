using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftBeperkingController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftBeperkingController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftBeperking/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Ecode)
        {

            var heeftBeperkingen = _dbContext.HeeftBeperkingen
                .Where(hb => hb.Ecode == Ecode).ToList();
            
            return Ok(heeftBeperkingen);
        }

        
        // POST: api/HeeftBeperking/?
        [HttpPost]
        public IActionResult Post([FromBody] HeeftBeperkingRequestModel requestModel)
        {
            HeeftBeperking newHeeftBeperking = new HeeftBeperking()
            {
                Bcode = requestModel.Bcode,
                Ecode = requestModel.Ecode
            };

            _dbContext.HeeftBeperkingen.Add(newHeeftBeperking);
            _dbContext.SaveChanges();
            return StatusCode(201, newHeeftBeperking);
        }

        // DELETE: api/HeeftBeperking/?
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftBeperkingRequestModel requestModel)
        {
            HeeftBeperking? heeftBeperkingToDelete = _dbContext.HeeftBeperkingen
                .SingleOrDefault(hb => hb.Ecode == requestModel.Ecode && hb.Bcode == requestModel.Bcode);

            if (heeftBeperkingToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.HeeftBeperkingen.Remove(heeftBeperkingToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}