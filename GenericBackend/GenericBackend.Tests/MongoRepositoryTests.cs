using System;
using GenericBackend.DataModels.Document;
using GenericBackend.Repository;
using NUnit.Framework;

namespace GenericBackend.Tests
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        [SetUp]
        public void Init()
        {
            
        }
        [Test]
        public void Populate_FakeDucuments_ShouldAddDocuments()
        {
            var date = DateTime.UtcNow.Date.AddDays(-20);
            var unitOfWork = new UnitOfWork.GoodNightMedical.UnitOfWork();

            for (int i = 0; i < 10; i++)
            {
                var doc = new DocumentInfo
                {
                    User = "superuser@example.com",
                    Name = "Plan " + i,
                    Type = "Type",
                    DateOfPost = date.AddDays(i).AddMinutes(i * 15)
                };
                unitOfWork.DocumentsInfo.Add(doc);
            }

            for (int i = 0; i < 10; i++)
            {
                var doc = new DocumentInfo
                {
                    User = "demouser@example.com",
                    Name = "Report " + i,
                    Type = "Type 2",
                    DateOfPost = date.AddDays(i).AddMinutes(i * 20)
                };
                unitOfWork.DocumentsInfo.Add(doc);
            }
        }
        [Test]
        public void Add_ForMongoDb_ShouldAddEntity_Test()
        {
            //var repo = new MongoRepository<Machine>();
            //var machineName = "TestMachine";
            
            //var machine = new Machine()
            //{
            //    Name = machineName ,
            //    ImageUrl = "http://www.goodnightmedical.com/images/cpap-machine-photos/Fisher-Paykel-Icon1.jpg",
            //    PricePerMonth = 100m
            //};
            ///*var rentOptions = new List<RentOption>();
            //rentOptions.Add(new RentOption() { Name = "Rent 1", PricePerMonth = 100m });
            //rentOptions.Add(new RentOption() { Name = "Rent 2", PricePerMonth = 200m });
            //rentOptions.Add(new RentOption() { Name = "Rent 3", PricePerMonth = 300m });
            //machine.RentOptions = rentOptions;*/
            //repo.Add(machine);
            
            //Assert.That(repo.Any(x => x.Name == machineName));
        }
    }
}



