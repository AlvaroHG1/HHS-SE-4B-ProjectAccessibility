using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeeftVoogdController : ControllerBase
    {
        private readonly GebruikerContext _dbContext;

        public HeeftVoogdController(GebruikerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/HeeftVoogd/?
        [HttpGet("{id}")]
        public IActionResult Get(int Ecode)
        {

            var heeftVoogden = _dbContext.HeeftVoogden
                .Where(hv => hv.Ecode == Ecode).ToList();
            
            
            return Ok(heeftVoogden);
        }
        
        // DELETE: api/HeeftVoogd/?
        [HttpDelete]
        public IActionResult Delete([FromBody] HeeftVoogdRequestModel requestModel)
        {
            HeeftVoogd? heeftVoogdToDelete = _dbContext.HeeftVoogden
                .SingleOrDefault(ha => ha.Ecode == requestModel.Ecode && ha.Vcode == requestModel.Vcode);

            if (heeftVoogdToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _dbContext.HeeftVoogden.Remove(heeftVoogdToDelete);

            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
