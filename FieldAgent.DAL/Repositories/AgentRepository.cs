using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        public DBFactory DbFac { get; set; }

        public AgentRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agent.Add(agent);
                    db.SaveChanges();
                    response.Data = agent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response Update(Agent agent)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agent.Update(agent);
                    db.SaveChanges();
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response Delete(int agentId)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    foreach (AgencyAgent a in db.AgencyAgent.Where(a => a.AgentId == agentId).ToList())
                    {
                        db.AgencyAgent.Remove(a);
                    }
                    foreach (Alias a in db.Alias.Where(a => a.AgentId == agentId).ToList())
                    {
                        db.Alias.Remove(a);
                    }
                    foreach (Mission m in db.Mission.Where(m => m.AgentId == agentId).ToList())
                    {
                        db.Mission.Remove(m);
                    }
                    var agent = db.Agent.Find(agentId);
                    db.Agent.Remove(agent);
                    db.SaveChanges();
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agent = db.Agent.Find(agentId);
                    response.Data = agent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<List<Mission>> GetMissions(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            
            using (var db = DbFac.GetDbContext())
            {
                var missions = db.Mission.Where(m => m.AgentId == agentId).ToList();
                response.Data = missions;
                return response;
            }
        }
    }
}
