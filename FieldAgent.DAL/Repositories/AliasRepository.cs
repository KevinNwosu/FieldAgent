using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class AliasRepository : IAliasRepository
    {
        public DBFactory DbFac { get; set; }

        public AliasRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Alias.Add(alias);
                    db.SaveChanges();
                    response.Data = alias;
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

        public Response Update(Alias alias)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Alias.Update(alias);
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

        public Response Delete(int aliasId)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = db.Alias.Find(aliasId);
                    db.Alias.Remove(alias);
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

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = db.Alias.Find(aliasId);
                    response.Data = alias;
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

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> response = new Response<List<Alias>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = db.Alias.Include(a => a.Agent).Where(a => a.AgentId == agentId).ToList();
                    response.Data = alias;
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
