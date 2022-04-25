using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Test
{
    public class AgencyAgentRepositoryTest
    {
        AgencyAgentRepository db;
        DBFactory dbf;
        DateTime date = new DateTime(2002, 01, 01);
        DateTime NewDate = new DateTime(2004, 01, 01);

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void GetAgencyAgent()
        {
            var agencyAgent = db.Get(2, 1);
            Assert.AreEqual(agencyAgent.Data.ActivationDate, date);
        }
        [Test]
        public void GetByAgency()
        {
            var agencyAgents = db.GetByAgency(4);
            Assert.AreEqual(agencyAgents.Data.Count, 3);
        }
        [Test]
        public void GetByAgent()
        {
            var agencyAgents = db.GetByAgent(3);
            Assert.AreEqual(agencyAgents.Data.Count, 3);
        }
        [Test]
        public void InsertWorks()
        {
            var agencyAgent = new AgencyAgent()
            {
                AgencyId = 5,
                AgentId = 11,
                SecurityClearanceId = 4,
                BadgeId = Guid.NewGuid(),
                ActivationDate = date,
                DeactivationDate = null,
                IsActive = true
            };
            var result = db.Insert(agencyAgent);
            Assert.AreEqual(result.Data.ActivationDate, db.Get(5, 11).Data.ActivationDate);
        }
        [Test]
        public void UpdateWorks()
        {
            var agencyAgent = new AgencyAgent()
            {
                AgencyId = 4,
                AgentId = 8,
                SecurityClearanceId = 4,
                BadgeId = Guid.Parse("8bc13cd4-1952-4212-8ddb-68671864b3c8"),
                ActivationDate = NewDate,
                DeactivationDate = null,
                IsActive = true
            };
            var result = db.Update(agencyAgent);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(NewDate, db.Get(4, 8).Data.ActivationDate);
        }
        [Test]
        public void DeleteWorks()
        {
            var result = db.Delete(4, 8);
            Assert.IsTrue(result.Success);
        }
    }
}
