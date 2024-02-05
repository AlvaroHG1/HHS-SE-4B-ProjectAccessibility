using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;
using BCrypt.Net;

namespace ProjectAccessibility.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckWachtwoordController : ControllerBase
{
    private readonly GebruikerContext _dbContext;

    public CheckWachtwoordController(GebruikerContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/CheckWachtwoord?Email={Email}&Wachtwoord={Wachtwoord
    [HttpGet("{Email},{Wachtwoord}")]
    public IActionResult Get(String Email, String Wachtwoord)
    {
        var user = _dbContext.Gebruikers.SingleOrDefault(g => g.Email == Email);
        if (user == null)
        {
            return NotFound("Gebruiker niet gevonden");
        }
        // Vergelijk de gehashte wachtwoorden
        if (Utils.PasswordMatch(Wachtwoord, user.Wachtwoord))
        {
            return Ok(true); // Wachtwoord correct
        }
        else
        {
            return Ok(false); // Wachtwoord onjuist
        }
    }
}