using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.DAL.Test
{
    public class AgencyRepositoryTests
    {
        AgencyRepository db;
        DBFactory dbf;
        MissionRepository missionRepo;
        Agency Odin = new Agency
        {
            ShortName = "ODIN",
            LongName = "The Organization of Democratic Intelligence Networks"
        };
        Agency FBI = new Agency
        {
            ShortName = "FBI",
            LongName = "Federal Bureau of Investigation"
        };
        Agency ISIS = new Agency
        {
            AgencyId = 2,
            ShortName = "ISIS",
            LongName = "The International Secret Intelligence Service"
        };
        Agency Pink = new Agency
        {
            ShortName = "Pink",
            LongName = "King Inc"
        };
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            missionRepo = new MissionRepository(dbf);
            db = new AgencyRepository(dbf, missionRepo);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void GetAll()
        {
            Assert.AreEqual(5, db.GetAll().Data.Count);
        }

        [Test]
        public void GetFindsCorrectData()
        {
            Assert.AreEqual(Odin.LongName, db.Get(1).Data.LongName);
        }
        
        [Test]
        public void GetFindsNoData()
        {
            Assert.IsNull(db.Get(10).Data);
        }
        
        [Test]
        public void InsertAddsToDatabase()
        {
            db.Insert(Pink);
            Assert.AreEqual(6, db.GetAll().Data.Count);
        }
        
        [Test]
        public void UpdateUpdatesDatabase()
        {
            var status = db.Update(ISIS);
            Assert.AreEqual(ISIS.LongName, db.Get(2).Data.LongName);
            Assert.IsTrue(status.Success);
        }

        [Test]
        public void DeleteRemovesAgency()
        {
            var status = db.Delete(ISIS.AgencyId);
            Assert.IsTrue(status.Success);
            Assert.AreEqual(4, db.GetAll().Data.Count);
        }
    }
}