using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDeployment.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDeployment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PlayersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<PlayersController>

        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return _context.Players.ToList();
        }

        // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        [Authorize (Roles = "Admin")]
        public ActionResult<Player> Get(string id)
        {
            var player = _context.Players.FirstOrDefault(x => x.Username == id);
            if(player == null)
            {
                return NotFound("Player not found");
            }
            return Ok(player);
        }

        // POST api/<PlayersController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Post([FromBody] Player player)
        {
            if (_context.Players.FirstOrDefault(x => x.Username == player.Username) != null)
            {
                return Conflict();
            }

            _context.Players.Add(player);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("Get", new { id = player.Username }, player);
        }

        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Put(string id, [FromBody] Player value)
        {
            if (id != value.Username)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Players.FirstOrDefault(x => x.Username == value.Username) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(value);
        }

        // DELETE api/<PlayersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Delete(string id)
        {
            var player = _context.Players.FirstOrDefault(x => x.Username == id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
