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
    public class AgentRepositoryTests
    {
        AgentRepository db;
        DBFactory dbf;
        Agent InsertedAgent = new Agent()
        {
            FirstName = "Test",
            LastName = "Test",
            DateOfBirth = DateTime.Now,
            Height = 5.5M
        };
        Agent UpdatedAgent = new Agent()
        {
            AgentId = 10,
            FirstName = "Test",
            LastName = "Test",
            DateOfBirth = DateTime.Now,
            Height = 5.5M
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void GetAgent()
        {
            var agent = db.Get(1);
            Assert.AreEqual(6.1, agent.Data.Height);
        }
        [Test]
        public void InsertWorks()
        {
            var agent = db.Insert(InsertedAgent);
            Assert.AreEqual(InsertedAgent.FirstName, agent.Data.FirstName); 
        }
        [Test]
        public void UpdateWorks()
        {
            var agent = db.Update(UpdatedAgent);
            Assert.AreEqual(UpdatedAgent.FirstName, db.Get(10).Data.FirstName);
        }
        [Test]
        public void DeleteWorks()
        {
            var agent = db.Delete(10);
            Assert.IsTrue(agent.Success);
        }
        [Test]
        public void GetMissionsWorks()
        {
            var missions = db.GetMissions(1);
            Assert.AreEqual(4, missions.Data.Count());
        }
    }
}
