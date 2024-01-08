using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftAandoeningController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftAandoeningController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftAandoening/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Ecode)
        {

            var heeftAandoeningen = _dbContext.HeeftAandoeningen
                .Where(ha => ha.Ecode == Ecode).ToList();
            
            return Ok(heeftAandoeningen);
        }

        [HttpPost]
        public IActionResult Post([FromBody] HeeftAandoeningRequestModel requestModel)
        {
            HeeftAandoening newHeeftAandoening = new HeeftAandoening()
            {
                Acode = requestModel.Acode,
                Ecode = requestModel.Ecode
            };

            _dbContext.HeeftAandoeningen.Add(newHeeftAandoening);
            _dbContext.SaveChanges();
            return StatusCode(201, newHeeftAandoening);
        }

        // DELETE: api/HeeftGebruiker/5
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftAandoeningRequestModel requestModel)
        {
            HeeftAandoening? heeftAandoeningToDelete = _dbContext.HeeftAandoeningen
                .SingleOrDefault(ha => ha.Ecode == requestModel.Ecode && ha.Acode == requestModel.Acode);

            if (heeftAandoeningToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.HeeftAandoeningen.Remove(heeftAandoeningToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}