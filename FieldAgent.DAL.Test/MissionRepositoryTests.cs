using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Test
{
    public class MissionRepositoryTests
    {
        MissionRepository db;
        DBFactory dbf;
        Mission InsertMission = new Mission()
        {
            AgencyId = 1,
            CodeName = "Test Mission",
            Notes = "Test Mission Notes",
            StartDate = DateTime.Now,
            ProjectedEndDate = DateTime.Now.AddDays(100),
            ActualEndDate = null,
            OperationalCost = null
        };
        Mission UpdateMission = new Mission()
        {
            MissionId = 1,
            AgencyId = 1,
            CodeName = "Test Mission",
            Notes = "Test Mission Notes",
            StartDate = DateTime.Now,
            ProjectedEndDate = DateTime.Now.AddDays(100),
            ActualEndDate = null,
            OperationalCost = null
        };
        
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new MissionRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void GetWorks()
        {
            var result = db.Get(1);
            Assert.AreEqual("Mole Hunt", result.Data.CodeName);
        }
        [Test]
        public void GetByAgencyWorks()
        {
            var result = db.GetByAgency(4);
            Assert.AreEqual(6, result.Data.Count);
        }
        [Test]
        public void GetByAgentWorks()
        {
            var result = db.GetByAgent(1);
            Assert.AreEqual(4, result.Data.Count);
        }
        [Test]
        public void InsertWorks()
        {
            var result = db.Insert(InsertMission);
            Assert.AreEqual(InsertMission.CodeName, db.Get(17).Data.CodeName);
        }
        [Test]
        public void UpdateWorks()
        {
            var result = db.Update(UpdateMission);
            Assert.AreEqual(UpdateMission.CodeName, db.Get(1).Data.CodeName);
        }
        [Test]
        public void DeleteWorks()
        {
            var result = db.Delete(12);
            Assert.AreEqual(5, db.GetByAgency(4).Data.Count);
        }
    }
}
