using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OnderzoeksresultaatController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public OnderzoeksresultaatController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {

        Onderzoeksresultaat onderzoeksresultaat = _dbContext.Onderzoeksresultaten    
            .Single(or => or.Orcode == id);
        
        
        return Ok(onderzoeksresultaat);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] OnderzoeksresultaatRequestModel requestModel)
    {
        Onderzoeksresultaat newOnderzoeksresultaat = new Onderzoeksresultaat()
        {
           Ocode = requestModel.Ocode,
           Ecode = requestModel.Ecode,
           Datum = requestModel.Datum,
           Antwoord = requestModel.Antwoord,
           
        };

        _dbContext.Onderzoeksresultaten.Add(newOnderzoeksresultaat);
        _dbContext.SaveChanges();
        return StatusCode(201, newOnderzoeksresultaat);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] OnderzoeksresultaatRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest();
        }
        
        var existingOnderzoeksresultaat = _dbContext.Onderzoeksresultaten.Find(id);

        if (existingOnderzoeksresultaat == null)
        {
            return NotFound();
        }

        existingOnderzoeksresultaat.Ocode = requestModel.Ocode;
        existingOnderzoeksresultaat.Ecode = requestModel.Ecode;
        existingOnderzoeksresultaat.Datum = requestModel.Datum;
        existingOnderzoeksresultaat.Antwoord = requestModel.Antwoord;

        _dbContext.Entry(existingOnderzoeksresultaat).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Ok(existingOnderzoeksresultaat);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var onderzoeksresultaatToDelete = _dbContext.Onderzoeksresultaten.Find(id);

        if (onderzoeksresultaatToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Onderzoeksresultaten.Remove(onderzoeksresultaatToDelete);

        _dbContext.SaveChanges();

        return NoContent(); 
    }
}