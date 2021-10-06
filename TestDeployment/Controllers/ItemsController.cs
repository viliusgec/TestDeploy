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
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>
        {
            new Item{ Id = 1, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Axe", Requirements = "Mining:1" },
            new Item{ Id = 2, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Pick", Requirements = "Mining:1" },
            new Item{ Id = 3, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Showel", Requirements = "Mining:1" },
            new Item{ Id = 4, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Axe", Requirements = "Mining:1" },
            new Item{ Id = 5, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Pick", Requirements = "Mining:1" },
            new Item{ Id = 6, Ammount = 1, IsEquiped = true, Description = "Item", Name = "Pick", Requirements = "Mining:1" }
        };

        private static readonly List<PlayerItems> playerItems = new List<PlayerItems>
        {
            new PlayerItems{ PlayerName = "PlayerOne", ItemId = 1},
            new PlayerItems{ PlayerName = "PlayerOne", ItemId = 2},
            new PlayerItems{ PlayerName = "PlayerOne", ItemId = 3},
            new PlayerItems{ PlayerName = "PlayerTwo", ItemId = 4},
            new PlayerItems{ PlayerName = "PlayerTwo", ItemId = 5},
            new PlayerItems{ PlayerName = "PlayerThree", ItemId = 6},
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
        public ActionResult<IEnumerable<Item>> Get(string playerName)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var playerItem = playerItems.Where(x => x.PlayerName == playerName).ToList();
            var itemsdto = new List<Item>();
            foreach (var item in playerItem)
            {
                itemsdto.Add(items.First(x => x.Id == item.ItemId));
            }
            return itemsdto;
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
            var playerItem = playerItems.FirstOrDefault(x => x.PlayerName == playerName && x.ItemId == id);
            if (playerItem == null)
            {
                return NotFound("Item not found");
            }
            return Ok(items.First(x => x.Id == playerItem.ItemId));
        }

        // POST api/<SkillsController>
        [HttpPost]
        public ActionResult<Skill> Post(string playerName, [FromBody] Item item)
        {
            var player = players.FirstOrDefault(a => a.Username == playerName);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            if (playerItems.FirstOrDefault(x => x.PlayerName == playerName && x.ItemId == item.Id) != null)
            {
                return Conflict();
            }
            var playerItem = new PlayerItems { ItemId = item.Id, PlayerName = playerName};
            playerItems.Add(playerItem);
            items.Add(item);
            return CreatedAtAction("Get", new { playerName = playerName, id = item.Id }, item);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        public ActionResult<Player> Put(string playerName, int id, [FromBody] Item value)
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
            var playerItem = playerItems.FirstOrDefault(x => x.PlayerName == playerName && x.ItemId == id);
            if (playerItem == null)
            {
                return NotFound("Item not found");
            }

            items.Remove(items.First(x => x.Id == playerItem.ItemId));
            items.Add(value);
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
            var playerItem = playerItems.FirstOrDefault(x => x.PlayerName == playerName && x.ItemId == id);
            if (playerItem == null)
            {
                return NotFound("Item not found");
            }
            playerItems.Remove(playerItem);
            items.Remove(items.First(x => x.Id == playerItem.ItemId));
            return NoContent();
        }
    }
}
