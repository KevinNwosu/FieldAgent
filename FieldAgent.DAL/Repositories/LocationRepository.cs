using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private DbContextOptions dbco;

        public LocationRepository(FactoryMode mode = FactoryMode.TEST)
        {
            dbco = DBFactory.GetDbContext(mode);
        }

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new Response<Location>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Location.Add(location);
                    db.SaveChanges();
                    response.Data = location;
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

        public Response Update(Location location)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    db.Location.Update(location);
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

        public Response Delete(int locationId)
        {
            Response response = new Response();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var location = db.Location.Find(locationId);
                    db.Location.Remove(location);
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

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new Response<Location>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var location = db.Location.Find(locationId);
                    if (location != null)
                    {
                        response.Data = location;
                        response.Success = true;
                        return response;
                    }
                    else
                    {
                        response.Message = "Location not found";
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

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new Response<List<Location>>();

            try
            {
                using (var db = new AppDbContext(dbco))
                {
                    var location = db.Location.Include(a => a.Agency).Where(a => a.AgencyId == agencyId).ToList();
                    if (location != null)
                    {
                        response.Data = location;
                        response.Success = true;
                        return response;
                    }
                    else
                    {
                        response.Message = "Location not found";
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
