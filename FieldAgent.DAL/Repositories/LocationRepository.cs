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
    public class LocationRepository : ILocationRepository
    {
        public DBFactory DbFac { get; set; }

        public LocationRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new Response<Location>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Location.Add(location);
                    db.SaveChanges();
                    response.Data = location;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response Update(Location location)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Location.Update(location);
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

        public Response Delete(int locationId)
        {
            Response response = new Response();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var location = db.Location.Find(locationId);
                    db.Location.Remove(location);
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

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new Response<Location>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var location = db.Location.Find(locationId);
                    response.Data = location;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddMessage(ex.Message);
                return response;
            }
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new Response<List<Location>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var location = db.Location.Include(a => a.Agency).Where(a => a.AgencyId == agencyId).ToList();
                    response.Data = location;
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
