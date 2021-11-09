using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDeployment.Models;

namespace TestDeployment.Controllers
{
    [Route("api/Player/{playerName}/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<SkillsController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Item>> Get(string playerName)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            return _context.Items.Where(x => x.PlayerName == playerName).ToList();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Skill> Get(string playerName, int id)
        {
            /*            Item playerItem = _context.PlayerItems.Where(c => c.PlayerName == playerName && c.ItemId == id)
                                            .Join(
                                            _context.Items,
                                                playerItem => playerItem.ItemId,
                                                item => item.Id,
                                                (playerItem, item) => item).First();*/
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = _context.Items.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            return Ok(skill);
        }

        // POST api/<SkillsController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Skill> Post(string playerName, [FromBody] Item item)
        {
            var player = _context.Players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }

            if (_context.Items.FirstOrDefault(x => x.PlayerName == playerName && x.Id == item.Id) != null)
            {
                return Conflict();
            }
            item.PlayerName = playerName;

            _context.Items.Add(item);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("Get", new { playerName, id = item.Id }, item);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Player> Put(string playerName, int id, [FromBody] Item value)
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
                var skill = _context.Items.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
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
            var skill = _context.Items.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            _context.Items.Remove(skill);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
