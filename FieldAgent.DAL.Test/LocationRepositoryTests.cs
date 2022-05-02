using System;
using FieldAgent.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL.Test
{
    public class LocationRepositoryTests
    {
        LocationRepository db;
        Location Location = new Location()
        {
            AgencyId = 2,
            LocationName = "Test Location",
            Street1 = "Test Street",
            Street2 = "Test Street 2",
            City = "Test City",
            PostalCode = "TPC",
            CountryCode = "TC"
        };
        Location UpdateLocation = new Location()
        {
            LocationId = 1,
            AgencyId = 1,
            LocationName = "Test Location",
            Street1 = "Test Street",
            Street2 = "Test Street 2",
            City = "Test City",
            PostalCode = "TPC",
            CountryCode = "TC"
        };

        [SetUp]
        public void Setup()
        {
            LocationRepository setup = new LocationRepository(FactoryMode.TEST);
            setup.SetKnownGoodState();
            db = setup;
        }
        [Test]
        public void GetWorks()
        {
            Assert.AreEqual("ODIN Headquarters", db.Get(1).Data.LocationName);
        }
        [Test]
        public void GetByAgencyWorks()
        {
            Assert.AreEqual(1, db.GetByAgency(1).Data.Count);
        }
        [Test]
        public void InsertWorks()
        {
            var location = db.Insert(Location);
            Assert.IsTrue(location.Success);
            Assert.AreEqual(Location.City, db.Get(6).Data.City);
        }
        [Test]
        public void UpdateWorks()
        {
            db.Update(UpdateLocation);
            Assert.AreEqual("Test Location", db.Get(1).Data.LocationName);
        }
        [Test]
        public void DeleteWorks()
        {
            db.Delete(5);
            Assert.AreEqual(0, db.GetByAgency(5).Data.Count);
        }
    }
}
