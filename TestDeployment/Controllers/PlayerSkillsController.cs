using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // GET: api/<SkillsController>
        [HttpGet]
        public ActionResult<IEnumerable<Skill>> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        public ActionResult<Skill> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<SkillsController>
        [HttpPost]
        public ActionResult<Skill> Post([FromBody] Skill skill)
        {
            throw new NotImplementedException();
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        public ActionResult<Player> Put(int id, [FromBody] Skill value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<SkillsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Player> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
