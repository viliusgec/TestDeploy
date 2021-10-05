using Microsoft.AspNetCore.Mvc;
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
        private static readonly List<Player> players = new List<Player>
        {
           new Player{ Username = "PlayerOne", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerTwo", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerThree", InventorySpace=5, BattleState = true, Money = 500 },
           new Player{ Username = "PlayerFour", InventorySpace=5, BattleState = true, Money = 500 },
        };

        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return players;
        }

        // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        public ActionResult<Player> Get(string id)
        {
            var player = players.FirstOrDefault(a => a.Username == id);
            if(player == null)
            {
                return NotFound("Player not found");
            }
            return Ok(player);
        }

        // POST api/<PlayersController>
        [HttpPost]
        public ActionResult<Player> Post([FromBody] Player player)
        {
            if (players.FirstOrDefault(x => x.Username == player.Username) != null)
            {
                return Conflict();
            }
            players.Add(player);
            return CreatedAtAction("Get", new { id = player.Username }, player);
        }

        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        public ActionResult<Player> Put(string id, [FromBody] Player value)
        {
            if (id != value.Username)
            {
                return BadRequest();
            }
            var player = players.FirstOrDefault(a => a.Username == id);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            players.Remove(player);
            players.Add(value);
            return Ok(value);
        }

        // DELETE api/<PlayersController>/5
        [HttpDelete("{id}")]
        public ActionResult<Player> Delete(string id)
        {
            var player = players.FirstOrDefault(a => a.Username == id);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            players.Remove(player);
            return NoContent();
        }
    }
}
