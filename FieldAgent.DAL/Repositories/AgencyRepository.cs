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
                var agency = db.Agency.ToList();
                if (agency.Count > 0)
                {
                    response.Data = agency;
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
                var agency = db.Agency.Find(agencyId);
                if (agency != null)
                {
                    response.Data = agency;
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
            Response response = new Response();
            using (var db = DbFac.GetDbContext())
            {
                db.Agency.Update(agency);
                db.SaveChanges();
            }
            return response;
        }
        public Response Delete(int agencyId)
        {
            Response response = new Response();
            using (var db = DbFac.GetDbContext())
            {
                var agency = db.Agency.Find(agencyId);
                db.Agency.Remove(agency);
                db.SaveChanges();
            }
            return response;
        }
    }
}
