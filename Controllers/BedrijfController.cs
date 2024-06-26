using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;
using ProjectAccessibility.Models.RequestModels;
using ProjectAccessibility.Models.ReturnModels;


namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public BedrijfController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bedrijven = _dbContext.Bedrijven.OrderBy(b => b.Gcode).ToList();
            return Ok(bedrijven);
        }
        
        // GET: api/Bedrijf/?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Bedrijf bedrijf = _dbContext.Bedrijven
                .Single(b => b.Gcode == id);
            
            BedrijfReturnModel returnModel = new BedrijfReturnModel()
            {
                Bedrijf = bedrijf,
            };
            
            return Ok(returnModel);

        }

        [HttpPost]
        public IActionResult Post([FromBody] BedrijfRequestModel requestModel)
        {
            Bedrijf newBedrijf = new Bedrijf()
            {
                Naam = requestModel.Naam,
                Link = requestModel.Link,
                Bedrijfsinformatie = requestModel.Bedrijfsinformatie,
                Email = requestModel.Email,
                Locatie = requestModel.Locatie,
                Wachtwoord = requestModel.Wachtwoord
            };

            _dbContext.Bedrijven.Add(newBedrijf);
            _dbContext.SaveChanges();
            return StatusCode(201, newBedrijf);
        }

        // PUT: api/Bedrijf/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BedrijfPutModel requestModel)
        {
            var existingBedrijf = _dbContext.Bedrijven.Find(id);

            if (existingBedrijf == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingBedrijf.Naam = requestModel.Naam;
            existingBedrijf.Link = requestModel.Link;
            existingBedrijf.Bedrijfsinformatie = requestModel.Bedrijfsinformatie;
            existingBedrijf.Locatie = requestModel.Locatie;
            existingBedrijf.Email = requestModel.Email;

            _dbContext.Entry(existingBedrijf).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(existingBedrijf); // 200 OK
        }

        // DELETE: api/Bedrijf/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bedrijfToDelete = _dbContext.Bedrijven.Find(id);

            if (bedrijfToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.Bedrijven.Remove(bedrijfToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}