namespace ProjectAccessibility.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

[Route("api/[controller]")]
[ApiController]
public class ErvaringdeskundigeController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public ErvaringdeskundigeController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Ervaringdeskundige/?
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {

        Ervaringdeskundige ervaringdeskundige = _dbContext.Ervaringdeskundiges
            .Single(e => e.Gcode == id);


        return Ok(ervaringdeskundige);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ErvaringdeskundigeRequestModel requestModel)
    {

        Ervaringdeskundige newErvaringdeskundige = new Ervaringdeskundige() 
        {
            Voornaam = requestModel.Voornaam,
            Achternaam = requestModel.Achternaam,
            Email = requestModel.Email,
            Wachtwoord = requestModel.Wachtwoord,
            Telefoonnummer = requestModel.Telefoonnummer,
            Straatnaam = requestModel.Straatnaam,
            Postcode = requestModel.Postcode,
            Huisnummer = requestModel.Huisnummer,
            Commercieel = requestModel.Commercieel,
            Contactvoorkeur = requestModel.Contactvoorkeur,
            Plaats = requestModel.Plaats
        };

        _dbContext.Ervaringdeskundiges.Add(newErvaringdeskundige);
        _dbContext.SaveChanges();
        return StatusCode(201, newErvaringdeskundige);
    }

    // PUT: api/Gebruiker/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ErvaringdeskundigeRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest();
        }
        
        var existingErvaringdeskundige = _dbContext.Ervaringdeskundiges.Find(id);

        if (existingErvaringdeskundige == null)
        {
            return NotFound(); // 404 Not Found
        }

        existingErvaringdeskundige.Voornaam = requestModel.Voornaam;
        existingErvaringdeskundige.Achternaam = requestModel.Achternaam;
        existingErvaringdeskundige.Email = requestModel.Email;
        existingErvaringdeskundige.Telefoonnummer = requestModel.Telefoonnummer;
        existingErvaringdeskundige.Wachtwoord = requestModel.Wachtwoord;
        existingErvaringdeskundige.Straatnaam = requestModel.Straatnaam;
        existingErvaringdeskundige.Postcode = requestModel.Postcode;
        existingErvaringdeskundige.Huisnummer = requestModel.Huisnummer;
        existingErvaringdeskundige.Commercieel = requestModel.Commercieel;
        existingErvaringdeskundige.Contactvoorkeur = requestModel.Contactvoorkeur;

        _dbContext.Entry(existingErvaringdeskundige).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Ok(existingErvaringdeskundige); // 200 OK
    }

    // DELETE: api/Gebruiker/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ervaringdeskundigeToDelete = _dbContext.Ervaringdeskundiges.Find(id);

        if (ervaringdeskundigeToDelete == null)
        {
            return NotFound(); // 404 Not Found
        }

        _dbContext.Ervaringdeskundiges.Remove(ervaringdeskundigeToDelete);

        _dbContext.SaveChanges();

        return NoContent(); // 204 No Content
    }
}

