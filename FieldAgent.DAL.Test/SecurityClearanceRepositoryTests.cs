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
    public class SecurityClearanceRepositoryTests
    {
        SecurityClearanceRepository db;
        DBFactory dbf;
        SecurityClearance securityClearance = new SecurityClearance
        {
            SecurityClearanceId = 3,
            SecurityClearanceName = "Secret"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new SecurityClearanceRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void TestGet()
        {
            Assert.AreEqual(securityClearance.SecurityClearanceName, db.Get(3).Data.SecurityClearanceName);
        }
        [Test]
        public void TestGetAll()
        {
            Assert.AreEqual(5, db.GetAll().Data.Count);
        }
    }
}
