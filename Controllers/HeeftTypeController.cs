using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftTypeController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftTypeController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftType/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Bcode)
        {
            
            var heeftOnderzoek = _dbContext.HeeftOnderzoeken
                .Where(ho => ho.Ocode == Bcode).ToList();
            
            return Ok(heeftOnderzoek);
        }

        // POST: api/HeeftType/?
        [HttpPost]
        public IActionResult Post([FromBody] HeeftTypeRequestModel requestModel)
        {
            HeeftType newHeeftType = new HeeftType()
            {
                Otcode = requestModel.Otcode,
                Ocode = requestModel.Ocode

            };

            _dbContext.HeeftTypes.Add(newHeeftType);
            _dbContext.SaveChanges();
            return StatusCode(201, newHeeftType);
        }

        // DELETE: api/HeeftType/5
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftTypeRequestModel requestModel)
        {
            HeeftType? heeftTypeToDelete = _dbContext.HeeftTypes
                .SingleOrDefault(ht => ht.Ocode == requestModel.Ocode && ht.Otcode == requestModel.Otcode);

            if (heeftTypeToDelete == null)
            {
                return NotFound();
            }

            _dbContext.HeeftTypes.Remove(heeftTypeToDelete);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}