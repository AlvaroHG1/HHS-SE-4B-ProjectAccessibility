using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoorkeurTypeController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public VoorkeurTypeController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        VoorkeurType voorkeurType = _dbContext.VoorkeurTypes    
            .Single(vt => vt.Ecode == id);
        
        return Ok(voorkeurType);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] VoorkeurTypeRequestModel requestModel)
    {
        VoorkeurType newVoorkeurtype = new VoorkeurType()
        {
            Otcode = requestModel.Otcode,
            Ecode = requestModel.Ecode
                   
        };

        _dbContext.VoorkeurTypes.Add(newVoorkeurtype);
        _dbContext.SaveChanges();
        return StatusCode(201, newVoorkeurtype);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var voorkeurTypeToDelete = _dbContext.VoorkeurTypes.Find(id);

        if (voorkeurTypeToDelete == null)
        {
            return NotFound();
        }

        _dbContext.VoorkeurTypes.Remove(voorkeurTypeToDelete);

        _dbContext.SaveChanges();

        return NoContent();
    }
}