using Microsoft.AspNetCore.Mvc;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly GebruikerRepository _gebruikerRepository;

        public GebruikerController(GebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: api/Gebruiker/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Gebruiker gebruiker = _gebruikerRepository.GetGebruikerById(id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        // POST: api/Gebruiker
        [HttpPost]
        public IActionResult Post([FromBody] GebruikerRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest();
            }

            _gebruikerRepository.CreateGebruiker(requestModel.Email, requestModel.Wachtwoord);

            return StatusCode(201);
        }

        // PUT: api/Gebruiker/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Gebruiker gebruiker)
        {
            if (gebruiker == null || id != gebruiker.Gcode)
            {
                return BadRequest(); // 400 Bad Request if the input is invalid
            }

            // Update the user in the database using the repository
            // Handle the result, e.g., return 204 No Content if the update is successful
            // or return 404 Not Found if the user with the given ID is not found
            // ...

            return NoContent(); // 204 No Content
        }

        // DELETE: api/Gebruiker/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Delete the user from the database using the repository
            // Handle the result, e.g., return 204 No Content if the delete is successful
            // or return 404 Not Found if the user with the given ID is not found
            // ...

            return NoContent(); // 204 No Content
        }
    }
}