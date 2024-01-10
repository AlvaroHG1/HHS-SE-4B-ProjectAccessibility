using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftOnderzoekController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftOnderzoekController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftOnderzoek/?
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Bcode)
        {
            
            var heeftOnderzoek = _dbContext.HeeftOnderzoeken
                .Where(ho => ho.Ocode == Bcode).ToList();
            
            return Ok(heeftOnderzoek);
        }

        [HttpPost]
        public IActionResult Post([FromBody] HeeftOnderzoekRequestModel requestModel)
        {
            HeeftOnderzoek newHeeftOnderzoek = new HeeftOnderzoek()
            {
                Ocode = requestModel.Ocode,
                Bcode = requestModel.Bcode
            };

            _dbContext.HeeftOnderzoeken.Add(newHeeftOnderzoek);
            _dbContext.SaveChanges();
            return StatusCode(201, newHeeftOnderzoek);
        }

        // DELETE: api/HeeftGebruiker/5
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftOnderzoekRequestModel requestModel)
        {
            HeeftOnderzoek? heeftOnderzoekToDelete = _dbContext.HeeftOnderzoeken
                .SingleOrDefault(ho => ho.Ocode == requestModel.Ocode && ho.Bcode == requestModel.Bcode);

            if (heeftOnderzoekToDelete == null)
            {
                return NotFound();
            }

            _dbContext.HeeftOnderzoeken.Remove(heeftOnderzoekToDelete);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}