using FieldAgent.API.Models;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }
        [HttpGet]
        [Route("/api/[controller]/{id}", Name = "GetAgent")]
        public IActionResult Get(int id)
        {
            var agent = _agentRepository.Get(id);
            if (!agent.Success)
            {
                return BadRequest(agent.Message);
            }
            return Ok(new AgentModel()
            {
                AgentId = agent.Data.AgentId,
                FirstName = agent.Data.FirstName,
                LastName = agent.Data.LastName,
                DateOfBirth = agent.Data.DateOfBirth,
                Height = agent.Data.Height,
            });
        }
    }
}
