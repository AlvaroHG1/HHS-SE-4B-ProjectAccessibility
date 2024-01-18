using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public LoginController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Hulpmiddel/?
    [HttpGet("{Email}, {Wachtwoord}")]
    public IActionResult Get(String Email, String Wachtwoord)
    {
        Gebruiker gebruiker = _dbContext.Gebruikers.Single(g => g.Email == Email && g.Wachtwoord == Wachtwoord);
        
        return Ok(gebruiker);
    }
    
}