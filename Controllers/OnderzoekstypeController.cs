using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OnderzoekstypeController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public OnderzoekstypeController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Onderzoekstype onderzoekstype = _dbContext.Onderzoekstypes    
            .Single(ot => ot.Otcode == id);
        
        return Ok(onderzoekstype);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] OnderzoekstypeRequestModel requestModel)
    {
        Onderzoekstype newOnderzoekstype = new Onderzoekstype()
        {
            Type = requestModel.Type 
                   
        };

        _dbContext.Onderzoekstypes.Add(newOnderzoekstype);
        _dbContext.SaveChanges();
        return StatusCode(201, newOnderzoekstype);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] OnderzoekstypeRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest();
        }
        
        var existingOnderzoekstype = _dbContext.Onderzoekstypes.Find(id);

        if (existingOnderzoekstype == null)
        {
            return NotFound(); 
        }
        
        existingOnderzoekstype.Type = requestModel.Type;

        _dbContext.Entry(existingOnderzoekstype).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Ok(existingOnderzoekstype);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var onderzoekstypeToDelete = _dbContext.Onderzoekstypes.Find(id);

        if (onderzoekstypeToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Onderzoekstypes.Remove(onderzoekstypeToDelete);

        _dbContext.SaveChanges();

        return NoContent();
    }
}