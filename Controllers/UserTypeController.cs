using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserTypeController : ControllerBase
{
    private readonly GebruikerContext _dbContext;
    
    public UserTypeController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // GET: api/UserType/?
    [HttpGet("{gcode}")]
    public IActionResult Get(int gcode)
    {
        Gebruiker? gebruiker = _dbContext.Gebruikers.Find(gcode);
        if (gebruiker is null)
        {
            return NotFound();
        }
        if (gebruiker is Ervaringdeskundige)
        {
            return Ok("Ervaringsdeskundige");
        }
        if (gebruiker is Bedrijf)
        {
            return Ok("Bedrijf");
        }
        if (gebruiker is Beheerder)
        {
            return Ok("Beheerder");
        }
        return NotFound();
    }
}