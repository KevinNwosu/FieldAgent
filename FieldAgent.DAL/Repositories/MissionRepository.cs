using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repositories
{
    public class MissionRepository : IMissionRepository
    {
        private DbContextOptions dbco;

        public MissionRepository(FactoryMode mode = FactoryMode.TEST)
        {
            dbco = DBFactory.GetDbContext(mode);
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Mission.Add(mission);
                    db.SaveChanges();
                    response.Data = mission;
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

        public Response Update(Mission mission)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Mission.Update(mission);
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

        public Response Delete(int missionId)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    foreach (MissionAgent ma in db.MissionAgent.Where(ma => ma.MissionId == missionId).ToList())
                    {
                        db.MissionAgent.Remove(ma);
                    }
                    var mission = db.Mission.Find(missionId);
                    db.Mission.Remove(mission);
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

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> response = new Response<Mission>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var mission = db.Mission.Find(missionId);
                    if (mission != null)
                    {
                        response.Data = mission;
                        response.Success = true;
                        return response;
                    }
                    else
                    {
                        response.Message = "Mission not found";
                        response.Success = false;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var missions = db.Mission.Include(a => a.Agency).Where(a => a.AgencyId == agencyId).ToList();
                    if (missions != null)
                    {
                        response.Data = missions;
                        response.Success = true;
                        return response;
                    }
                    else
                    {
                        response.Message = "Mission not found";
                        response.Success = false;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var mission = db.Mission.Include(m => m.MissionAgents).Where(m => m.MissionAgents.Any(a => a.AgentId == agentId)).ToList();
                    if (mission != null)
                    {
                        response.Data = mission;
                        response.Success = true;
                        return response;
                    }
                    else
                    {
                        response.Message = "Mission not found";
                        response.Success = false;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}
