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
            
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agency = db.Agency.ToList();
                    response.Data = agency;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }
        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();
            
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agency = db.Agency.Find(agencyId);
                    response.Data = agency;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }
        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agency.Add(agency);
                    db.SaveChanges();
                    response.Data = agency;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }
        public Response Update(Agency agency)
        {
            Response response = new Response();
            
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agency.Update(agency);
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
        public Response Delete(int agencyId)
        {
            Response response = new Response();
            
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    foreach(AgencyAgent a in db.AgencyAgent.Where(a => a.AgencyId == agencyId).ToList())
                    {
                        db.AgencyAgent.Remove(a);
                    }
                    foreach (Location l in db.Location.Where(l => l.AgencyId == agencyId).ToList())
                    {
                        db.Location.Remove(l);
                    }
                    foreach (Mission m in db.Mission.Where(m => m.AgencyId == agencyId).ToList())
                    {
                        db.Mission.Remove(m);
                    }
                    var agency = db.Agency.Find(agencyId);
                    db.Agency.Remove(agency);
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
