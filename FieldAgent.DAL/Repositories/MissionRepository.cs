﻿using FieldAgent.Core;
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
        public DBFactory DbFac { get; set; }

        public MissionRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();

            try
            {
                using (var db = DbFac.GetDbContext())
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
                using (var db = DbFac.GetDbContext())
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
                using (var db = DbFac.GetDbContext())
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
                using (var db = DbFac.GetDbContext())
                {
                    var mission = db.Mission.Find(missionId);
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

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var mission = db.Mission.Include(a => a.Agency).Where(a => a.AgencyId == agencyId).ToList();
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

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var mission = db.Mission.Include(m => m.MissionAgents).Where(m => m.MissionAgents.Any(a => a.AgentId == agentId)).ToList();
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
    }
}
