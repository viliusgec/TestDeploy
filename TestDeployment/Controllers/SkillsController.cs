using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDeployment.Models;

namespace TestDeployment.Controllers
{
    [Route("api/Player/{playerName}/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private static readonly List<Skill> skills = new List<Skill>
        {
            new Skill{ Id = 1, PlayerName = "PlayerOne", Experience = 1, SkillName = "Woodcutting"},
            new Skill{ Id = 2, PlayerName = "PlayerOne", Experience = 1, SkillName = "Mining"},
            new Skill{ Id = 3, PlayerName = "PlayerTwo", Experience = 1, SkillName = "Woodcutting"},
            new Skill{ Id = 4, PlayerName = "PlayerTwo", Experience = 1, SkillName = "Mining"},
            new Skill{ Id = 5, PlayerName = "PlayerTwo", Experience = 1, SkillName = "Fishing"}
        };

        private static readonly List<Player> players = new List<Player>
        {
           new Player{ Username = "PlayerOne", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerTwo", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerThree", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerFour", InventorySpace=5, BattleState = true, Money = 500 }
        };

        // GET: api/<SkillsController>
        [HttpGet]
        public ActionResult<IEnumerable<Skill>> Get(string playerName)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            return skills.Where(x => x.PlayerName == playerName).ToList();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        public ActionResult<Skill> Get(string playerName, int id)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            return Ok(skill);
        }

        // POST api/<SkillsController>
        [HttpPost]
        public ActionResult<Skill> Post(string playerName, [FromBody] Skill skill)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            if (skills.FirstOrDefault(x => x.PlayerName == playerName && x.Id == skill.Id) != null)
            {
                return Conflict();
            }
            skill.PlayerName = playerName;
            skills.Add(skill);
            return CreatedAtAction("Get", new { playerName = playerName, id = skill.Id }, skill);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        public ActionResult<Player> Put(string playerName, int id, [FromBody] Skill value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            skills.Remove(skill);
            skills.Add(value);
            return Ok(value);
        }

        // DELETE api/<SkillsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Player> Delete(string playerName, int id)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var skill = skills.FirstOrDefault(a => a.PlayerName == playerName && a.Id == id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            skills.Remove(skill);
            return NoContent();
        }
    }
}
