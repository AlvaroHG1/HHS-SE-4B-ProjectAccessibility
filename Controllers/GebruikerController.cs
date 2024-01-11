namespace ProjectAccessibility.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

[Route("api/[controller]")]
[ApiController]
public class GebruikerController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public GebruikerController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Ervaringdeskundige/?
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Gebruiker gebruiker = _dbContext.Gebruikers
            .Single(g => g.Gcode == id);
        
        return Ok(gebruiker);
    }
}

