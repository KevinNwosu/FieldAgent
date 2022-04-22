using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        public Response Delete(int agencyid, int agentid)
        {
            throw new NotImplementedException();
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            throw new NotImplementedException();
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            throw new NotImplementedException();
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            throw new NotImplementedException();
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            throw new NotImplementedException();
        }
    }
}
