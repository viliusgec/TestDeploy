using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestDeployment.Models;

namespace TestDeployment.Controllers
{
    [Route("api/Player/{playerName}/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public SkillsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<SkillsController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Skill>> Get(string playerName)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            return _context.Skills.Where(x => x.PlayerName == playerName).ToList();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Skill> Get(string playerName, int id)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = _context.Skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            return Ok(skill);
        }

        // POST api/<SkillsController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Skill> Post(string playerName, [FromBody] Skill skill)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }

            if (_context.Skills.FirstOrDefault(x => x.PlayerName == playerName && x.Id == skill.Id) != null)
            {
                return Conflict();
            }
            skill.PlayerName = playerName;

            _context.Skills.Add(skill);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("Get", new { playerName, id = skill.Id }, skill);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Put(string playerName, int id, [FromBody] Skill value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            value.PlayerName = playerName;

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var skill = _context.Skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
                if (skill == null)
                {
                    return NotFound("Skill not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok(value);
        }

        // DELETE api/<SkillsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Delete(string playerName, int id)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = _context.Skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
