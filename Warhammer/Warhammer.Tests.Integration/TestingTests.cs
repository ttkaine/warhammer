using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Warhammer.Core.Entities;

namespace Warhammer.Tests.Integration
{
    [TestFixture]
    public class TestingTests
    {
        [Test]
        [Ignore]
        public void TestPuttingPlaceInDb()
        {

            WarhammerDataEntities entities = new WarhammerDataEntities();
          
            Person person = new Person
            {
                FullName = "My Name", ShortName = "aaaaaaaaaaaaaanananana", IsDead = true
            , Created = DateTime.Now,
            Modified = DateTime.Now
            };
            person.Related.Add(new Session
            {
                FullName = "Where the bears rooooooooooom",
                ShortName = "Session 01",
                Created = DateTime.Now,
                Modified = DateTime.Now,
                DateTime = DateTime.Now
            });

            entities.Pages.Add(person);

            entities.SaveChanges();
        }
    }
}
