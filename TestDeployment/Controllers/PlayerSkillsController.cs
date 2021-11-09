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
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerSkillsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PlayerSkillsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<SkillsController>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Skill>> Get()
        {
            string Id = User.Claims.First(c => c.Type == "Username").Value;
            return _context.Skills.Where(x => x.PlayerName == Id).ToList();
        }

        [HttpGet]
        [Route("{id}/GetSelectedSkill")]
        public ActionResult<IEnumerable<Skill>> GetSelected(string id)
        {
            return _context.Skills.Where(x => x.SkillName == id).ToList();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Skill> Get(string id)
        {
            string Id = User.Claims.First(c => c.Type == "Username").Value;
            return _context.Skills.FirstOrDefault(x => x.PlayerName == Id && x.SkillName == id);
        }


        // POST api/<SkillsController>
        [HttpPost]
        [Authorize]
        public ActionResult<Skill> Post([FromBody] Skill skill)
        {
            string Id = User.Claims.First(c => c.Type == "Username").Value;
            skill.PlayerName = Id;

            if (_context.Skills.FirstOrDefault(x => (x.SkillName == skill.SkillName && x.PlayerName == Id) || x.Id == skill.Id) != null)
            {
                return Conflict();
            }

            _context.Skills.Add(skill);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("Get", new { id = skill.Id }, skill);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Player> Put(int id, [FromBody] Skill value)
        {
            string Id = User.Claims.First(c => c.Type == "Username").Value;
            if (id != value.Id)
            {
                return BadRequest();
            }
            value.PlayerName = Id;

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Skills.FirstOrDefault(x => x.Id == id && x.PlayerName == Id) == null)
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

        // DELETE api/<SkillsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Player> Delete(int id)
        {
            string Id = User.Claims.First(c => c.Type == "Username").Value;
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id && x.PlayerName ==  Id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
