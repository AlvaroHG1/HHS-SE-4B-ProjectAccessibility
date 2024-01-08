using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OnderzoekController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public OnderzoekController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {

        Onderzoek onderzoek = _dbContext.Onderzoeken    
            .Single(o => o.Ocode == id);
        
        
        return Ok(onderzoek);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] OnderzoekRequestModel requestModel)
    {
        Onderzoek newOnderzoek = new Onderzoek()
        { 
           Otcode = requestModel.Otcode,
           Titel = requestModel.Titel,
           Beschrijving = requestModel.Beschrijving,
           Locatie = requestModel.Locatie,
           Startdatum = requestModel.Startdatum,
           Einddatum = requestModel.Einddatum
           
        };

        _dbContext.Onderzoeken.Add(newOnderzoek);
        _dbContext.SaveChanges();
        return StatusCode(201, newOnderzoek);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] OnderzoekRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest();
        }
        
        var existingOnderzoek = _dbContext.Onderzoeken.Find(id);

        if (existingOnderzoek == null)
        {
            return NotFound();
        }

        existingOnderzoek.Otcode = requestModel.Otcode;
        existingOnderzoek.Titel = requestModel.Titel;
        existingOnderzoek.Beschrijving = requestModel.Beschrijving;
        existingOnderzoek.Locatie = requestModel.Locatie;
        existingOnderzoek.Startdatum = requestModel.Startdatum;
        existingOnderzoek.Einddatum = requestModel.Einddatum;

        _dbContext.Entry(existingOnderzoek).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Ok(existingOnderzoek);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var onderzoekToDelete = _dbContext.Onderzoeken.Find(id);

        if (onderzoekToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Onderzoeken.Remove(onderzoekToDelete);

        _dbContext.SaveChanges();

        return NoContent();
    }
}