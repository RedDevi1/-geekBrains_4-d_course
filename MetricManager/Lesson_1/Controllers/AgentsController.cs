using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private ValuesHolder _holder;

        public AgentsController (ValuesHolder holder)
        {
            _holder = holder;
        }
        [HttpPost("register")]
        public IActionResult RegisterAgent ([FromBody] AgentInfo agentInfo)
        {
            if (!_holder.Values.Contains(agentInfo))
            {
                _holder.Values.Add(agentInfo);
            }
            else
            {
                Console.WriteLine($"An agent with ID {agentInfo.AgentId} already exists.");
            }
            return Ok();
        }
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
        [HttpGet]
        public List<AgentInfo> GetAllAgents(ValuesHolder _holder)
        {
            return _holder.Values;
        }
    }
}
