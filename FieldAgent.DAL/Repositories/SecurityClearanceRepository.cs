using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repositories
{
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        public DBFactory DbFac { get; set; }

        public SecurityClearanceRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var securityClearance = db.SecurityClearance.Find(securityClearanceId);
                    response.Data = securityClearance;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var securityClearance = db.SecurityClearance.ToList();
                    response.Data = securityClearance;
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
