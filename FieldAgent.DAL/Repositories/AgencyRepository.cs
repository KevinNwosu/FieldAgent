using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }
        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();
            
            using (var db = DbFac.GetDbContext())
            {
                var stuff = db.Agency.ToList();
                if (stuff.Count > 0)
                {
                    response.Data = stuff;
                }
                else
                {
                    response.AddMessage("No data found");
                }
                return response;
            }
        }
        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                var stuff = db.Agency.Find(agencyId);
                if (stuff != null)
                {
                    response.Data = stuff;
                }
                else
                {
                    response.AddMessage("Agency not found");
                }
                return response;
            }
        }
        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                db.Agency.Add(agency);
                db.SaveChanges();
                response.Data = agency;
            }
            return response;
        }
        public Response Update(Agency agency)
        {
            throw new NotImplementedException();
        }
        public Response Delete(int agencyId)
        {
            throw new NotImplementedException();
        }
    }
}
