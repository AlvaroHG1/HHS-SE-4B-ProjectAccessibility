using Microsoft.AspNetCore.Mvc;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOnderzoekenController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public GetOnderzoekenController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Onderzoeken
        [HttpGet("{Ecode}")]
        public IActionResult Get(int Ecode)
        {
            Ervaringdeskundige ervaringdeskundige = _dbContext.Ervaringdeskundiges
                .Single(e => e.Gcode == Ecode);

            List<HeeftBeperking> beperkingcodes = _dbContext.HeeftBeperkingen
                .Where(hp => hp.Ecode == Ecode)
                .ToList();
            
            List<int> bCodes = beperkingcodes.Select(hp => hp.Bcode).ToList();

            List<Beperking> beperkingen = _dbContext.Beperkingen
                .Where(b => bCodes.Contains(b.Bcode))
                .ToList();
                
            List<Onderzoek> onderzoeken = _dbContext.Onderzoeken
                .AsEnumerable()
                .Where(o => o.MinLeeftijd < CalculateAge(ervaringdeskundige.Geboortedatum) &&
                            o.MaxLeeftijd > CalculateAge(ervaringdeskundige.Geboortedatum) &&
                            beperkingen.Any(b => b.Naam == o.GezochteBeperking))
                .ToList();
            return Ok(onderzoeken);
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}