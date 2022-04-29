using FieldAgent.API.Models;
using FieldAgent.Core.Entities;
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
        public IActionResult GetAgent(int id)
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
        [HttpGet]
        [Route("/api/[controller]/{id}/missions", Name = "GetAgentMissions")]
        public IActionResult GetAgentMissions(int id)
        {
            var agent = _agentRepository.Get(id);
            var missions = _agentRepository.GetMissions(id);
            if (!missions.Success)
            {
                return BadRequest(missions.Message);
            }
            else
            {
                return Ok(missions.Data.Select(
                    m => new MissionModel()
                    {
                        MissionId = m.MissionId,
                        CodeName = m.CodeName,
                        Notes = m.Notes,
                        StartDate = m.StartDate,
                        ProjectedEndDate = m.ProjectedEndDate,
                        ActualEndDate = m.ActualEndDate,
                        OperationalCost = m.OperationalCost,
                        AgencyId = m.AgencyId,
                    }));
            }   
        }
        [HttpPost]
        public IActionResult AddAgent(ViewAgentModel agent)
        {
            if (ModelState.IsValid)
            {
                Agent newAgent = new Agent()
                {
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    DateOfBirth = agent.DateOfBirth,
                    Height = agent.Height
                };
                
                var result = _agentRepository.Insert(newAgent);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
                else
                {
                    return CreatedAtRoute(nameof(GetAgent), new { id = result.Data.AgentId }, result.Data);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
