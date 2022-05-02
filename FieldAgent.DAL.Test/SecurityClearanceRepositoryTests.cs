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
        SecurityClearance securityClearance = new SecurityClearance
        {
            SecurityClearanceId = 3,
            SecurityClearanceName = "Secret"
        };

        [SetUp]
        public void Setup()
        {
            SecurityClearanceRepository setup = new SecurityClearanceRepository(FactoryMode.TEST);
            setup.SetKnownGoodState();
            db = setup;
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
