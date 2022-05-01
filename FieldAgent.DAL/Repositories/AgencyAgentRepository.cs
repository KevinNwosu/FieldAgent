using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        private DbContextOptions dbco;

        public AgencyAgentRepository(FactoryMode mode = FactoryMode.TEST)
        {
            dbco = DBFactory.GetDbContext(mode);
        }
        public Response Delete(int agencyid, int agentid)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agencyAgent = db.AgencyAgent.Find(agencyid, agentid);
                    db.AgencyAgent.Remove(agencyAgent);
                    db.SaveChanges();
                    response.Success = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agencyAgent = db.AgencyAgent.Where(a => a.AgencyId == agencyid && a.AgentId == agentid).FirstOrDefault();
                    if (agencyAgent != null)
                    {
                        response.Data = agencyAgent;
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = "Agency Agent not found";
                        response.Success = false;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agencyAgent = db.AgencyAgent.Include(a => a.Agency).Where(a => a.AgencyId == agencyId).ToList();
                    if (agencyAgent != null)
                    {
                        response.Data = agencyAgent;
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = "Agency Agent not found";
                        response.Success = false;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agencyAgent = db.AgencyAgent.Include(a => a.Agent).Where(a => a.AgentId == agentId).ToList();
                    if (agencyAgent != null)
                    {
                        response.Data = agencyAgent;
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = "Agency Agent not found";
                        response.Success = false;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.AgencyAgent.Add(agencyAgent);
                    db.SaveChanges();
                    response.Data = agencyAgent;
                    response.Success = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.AgencyAgent.Update(agencyAgent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
