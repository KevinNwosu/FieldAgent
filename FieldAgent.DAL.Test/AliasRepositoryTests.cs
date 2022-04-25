using FieldAgent.DAL.Repositories;
using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL.Test
{
    public class AliasRepositoryTests
    {
        AliasRepository db;
        DBFactory dbf;
        Alias InsertedAlias = new Alias()
        {
            AgentId = 1,
            AliasName = "Test Alias",
            InterpolId = Guid.NewGuid(),
            Persona = "Test Persona"
        };
        Alias UpdatedAlias = new Alias()
        {
            AliasId = 1,
            AgentId = 1,
            AliasName = "Test Alias",
            InterpolId = Guid.NewGuid(),
            Persona = "Test Persona"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AliasRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void GetWorks()
        {
            var result = db.Get(1);
            Assert.AreEqual("Duchess", result.Data.AliasName);
        }
        [Test]
        public void GetByAgentWorks()
        {
            var result = db.GetByAgent(4);
            Assert.AreEqual("Dame", result.Data[0].AliasName);
        }
        [Test]
        public void InsertWorks()
        {
            var alias = db.Insert(InsertedAlias);
            Assert.AreEqual(InsertedAlias.AliasName, alias.Data.AliasName);
        }
        [Test]
        public void UpdateWorks()
        {
            var alias = db.Update(UpdatedAlias);
            Assert.IsTrue(alias.Success);
        }
        [Test]
        public void DeleteWorks()
        {
            var alias = db.Delete(1);
            Assert.IsTrue(alias.Success);
        }
    }
}
