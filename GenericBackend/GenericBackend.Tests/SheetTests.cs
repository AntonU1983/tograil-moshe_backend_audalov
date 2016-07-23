using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core.Utils;
using GenericBackend.DataModels.Actual;
using GenericBackend.Identity;
using GenericBackend.Identity.Core;
using GenericBackend.Identity.Identity;
using GenericBackend.Repository.Sheets;
using NUnit.Framework;

namespace GenericBackend.Tests
{
    [TestFixture]
    public class SheetTests
    {
        [SetUp]
        public void Init()
        {

        }

        [Test]
        public void Add_ActualSheet_ShouldAddToRepository_Test()
        {
            var repo = new ActualSheetRepository();
            var sheetName = "Test";
            var actualSheet = new ActualSheet {Name = sheetName };
            var items = new List<ActualSheetItem>()
            {
                new ActualSheetItem
                {
                    Subject = "Test",
                    FirstUknknown = 12,
                    SecondUknknown = 13,
                    ThirdUknknown = 12,
                    Diff = 23,
                    FirstNis = 1,
                    SecondNis = 2,
                    TimelineData = new List<ActualTimelineData>
                    {
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        }
                    }
                },
                new ActualSheetItem
                {
                    Subject = "Test",
                    FirstUknknown = 12,
                    SecondUknknown = 13,
                    ThirdUknknown = 12,
                    Diff = 23,
                    FirstNis = 1,
                    SecondNis = 2,
                    TimelineData = new List<ActualTimelineData>
                    {
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        },
                        new ActualTimelineData
                        {
                            DateTime = new DateTime(1993, 05, 01),
                            AccumulatedActual = 12,
                            AccumulatedUpdate = 23,
                            Actual = 12,
                            UpdateActual = 132,
                            SupervisorComments = "test"
                        }
                    }
                }
            };
            actualSheet.ActualItems = items;
            repo.Add(actualSheet);

            Assert.That(repo.Any(x => x.Name == sheetName));
        }
    }
}
