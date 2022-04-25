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
    public class ReportsRepositoryTests
    {
        ReportsRepository db;
        DBFactory dbf;
        
        [SetUp]
        public void SetUp()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new ReportsRepository(cp.Config);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void GetTopAgentsWorks()
        {
            var result = db.GetTopAgents();
            Assert.AreEqual("Archer Sterling", result.Data[0].NameLastFirst);
            Assert.AreEqual("Slater ", result.Data[1].NameLastFirst);
            Assert.AreEqual("Troy Luke", result.Data[2].NameLastFirst);
        }
        [Test]
        public void GetPensionListWorks()
        {
            var result = db.GetPensionList(3);
            Assert.AreEqual("Jakov Nikolai", result.Data[0].NameLastFirst);
        }
        [Test]
        public void GetClearanceAuditWorks()
        {
            var result = db.AuditClearance(4, 4);
            Assert.AreEqual("Stern Conway", result.Data[0].NameLastFirst);
            Assert.AreEqual("Slater ", result.Data[1].NameLastFirst);
        }
    }
}
