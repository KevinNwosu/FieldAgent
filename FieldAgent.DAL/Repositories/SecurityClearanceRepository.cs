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
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        private DbContextOptions dbco;

        public SecurityClearanceRepository(FactoryMode mode = FactoryMode.TEST)
        {
            dbco = DBFactory.GetDbContext(mode);
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var securityClearance = db.SecurityClearance.Find(securityClearanceId);
                    response.Data = securityClearance;
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

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var securityClearance = db.SecurityClearance.ToList();
                    response.Data = securityClearance;
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
