using ProjectAccessibility.Models.ReturnModels;

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
        
        var beperkingNames = _dbContext.HeeftBeperkingen
            .Where(hb => hb.Ecode == id)
            .Join(
                _dbContext.Beperkingen,
                hb => hb.Bcode,
                b => b.Bcode,
                (hb, b) => b.Naam)
            .ToList();

        var hulpmiddelNames = _dbContext.HeeftHulpmiddelen
            .Where(hh => hh.Ecode == id)
            .Join(
                _dbContext.Hulpmiddelen,
                hh => hh.Hcode,
                h => h.Hcode,
                (hh, h) => h.Naam)
            .ToList();

        var aandoeningNames = _dbContext.HeeftAandoeningen
            .Where(ha => ha.Ecode == id)
            .Join(
                _dbContext.Aandoeningen,
                ha => ha.Acode,
                a => a.Acode,
                (ha, a) => a.Naam)
            .ToList();

        var voorkeurTypeNames = _dbContext.VoorkeurTypes
            .Where(vt => vt.Ecode == id)
            .Join(
                _dbContext.Onderzoekstypes,
                vt => vt.Otcode,
                ot => ot.Otcode,
                (vt, ot) => ot.Type)
            .ToList();
        
        ErvaringdeskundigeReturnModel returnModel = new ErvaringdeskundigeReturnModel()
        {
            Ervaringdeskundige = ervaringdeskundige,
            Beperkingen = beperkingNames,
            Hulpmiddellen = hulpmiddelNames,
            Aandoeningen = aandoeningNames,
            VoorkeurTypes = voorkeurTypeNames
        };
        

        return Ok(returnModel);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ErvaringdeskundigeRequestModel requestModel)
    {
        if (_dbContext.Gebruikers.SingleOrDefault(e => e.Email == requestModel.Email) != null)
        {
            return Problem("Email already exists");
        }

        Ervaringdeskundige newErvaringdeskundige = new Ervaringdeskundige() 
        {
            Voornaam = requestModel.Voornaam,
            Achternaam = requestModel.Achternaam,
            Email = requestModel.Email,
            Wachtwoord = Utils.HashPassword(requestModel.Wachtwoord),
            Telefoonnummer = requestModel.Telefoonnummer,
            Straatnaam = requestModel.Straatnaam,
            Postcode = requestModel.Postcode,
            Huisnummer = requestModel.Huisnummer,
            Commercieel = requestModel.Commercieel,
            Contactvoorkeur = requestModel.Contactvoorkeur,
            Plaats = requestModel.Plaats,
            Geboortedatum = requestModel.Geboortedatum
        };

        _dbContext.Ervaringdeskundiges.Add(newErvaringdeskundige);
        _dbContext.SaveChanges();
        return StatusCode(201, newErvaringdeskundige);
   }

    // PUT: api/Ervaringdeskundige/?
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
        existingErvaringdeskundige.Wachtwoord = Utils.HashPassword(requestModel.Wachtwoord);
        existingErvaringdeskundige.Straatnaam = requestModel.Straatnaam;
        existingErvaringdeskundige.Postcode = requestModel.Postcode;
        existingErvaringdeskundige.Huisnummer = requestModel.Huisnummer;
        existingErvaringdeskundige.Commercieel = requestModel.Commercieel;
        existingErvaringdeskundige.Contactvoorkeur = requestModel.Contactvoorkeur;
        existingErvaringdeskundige.Geboortedatum = requestModel.Geboortedatum;

        _dbContext.Entry(existingErvaringdeskundige).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Ok(existingErvaringdeskundige); // 200 OK
    }

    // DELETE: api/Ervaringdeskundige/5
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

