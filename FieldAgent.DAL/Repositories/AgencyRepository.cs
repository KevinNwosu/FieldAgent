using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        public MissionRepository MissionRepository { get; set; }

        private DbContextOptions dbco;

        public AgencyRepository(MissionRepository missionRepository, FactoryMode mode = FactoryMode.TEST)
        {
            dbco = DBFactory.GetDbContext(mode);
            MissionRepository = missionRepository;
        }
        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();
            
            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agency = db.Agency.ToList();
                    if (agency.Count > 0)
                    {
                        response.Data = agency;
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = "No agencies found";
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
        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();
            
            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var agency = db.Agency.Find(agencyId);
                    if (agency != null)
                    {
                        response.Data = agency;
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = "No agency found";
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
        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            
            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Agency.Add(agency);
                    db.SaveChanges();
                    response.Data = agency;
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
        public Response Update(Agency agency)
        {
            Response response = new Response();
            
            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Agency.Update(agency);
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
        public Response Delete(int agencyId)
        {
            Response response = new Response();
            
            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    foreach(AgencyAgent a in db.AgencyAgent.Where(a => a.AgencyId == agencyId).ToList())
                    {
                        db.AgencyAgent.Remove(a);
                    }
                    foreach (Location l in db.Location.Where(l => l.AgencyId == agencyId).ToList())
                    {
                        db.Location.Remove(l);
                    }
                    var missions = MissionRepository.GetByAgency(agencyId);
                    foreach (var m in missions.Data)
                    {
                        MissionRepository.Delete(m.MissionId);
                    }
                    var agency = db.Agency.Find(agencyId);
                    db.Agency.Remove(agency);
                    db.SaveChanges();
                    response.Success = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
        public void SetKnownGoodState()
        {
            using (var db = new AppDbContext(dbco))
            {
                db.Database.ExecuteSqlRaw("SetKnownGoodState");
            }
        }
    }
}
