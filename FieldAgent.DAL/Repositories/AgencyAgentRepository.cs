using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyAgentRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }
        public Response Delete(int agencyid, int agentid)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyAgent = db.AgencyAgent.Find(agencyid, agentid);
                    db.AgencyAgent.Remove(agencyAgent);
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

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyAgent = db.AgencyAgent.Find(agencyid, agentid);
                    response.Data = agencyAgent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyAgent = db.AgencyAgent.Where(a => a.AgencyId == agencyId).ToList();
                    response.Data = agencyAgent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyAgent = db.AgencyAgent.Where(a => a.AgentId == agentId).ToList();
                    response.Data = agencyAgent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgent.Add(agencyAgent);
                    db.SaveChanges();
                    response.Data = agencyAgent;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgent.Update(agencyAgent);
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
    }
}
