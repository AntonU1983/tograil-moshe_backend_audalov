using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Excel;
using NUnit.Framework;

namespace GenericBackend.Tests.IntegrationTests
{
    [TestFixture]
    public class ParsePlanActualTests
    {
        private string excelFilePath = @"C:\Projects\Misc\PlanActual.xlsx";

        [Test]
        public void ParseExelPlanActualFile_ShouldWork()
        {
            Assert.DoesNotThrow(() => ParsePlanActual.Parse(excelFilePath));
        }

        [Test]
        public void ParsePlanSheet_ForPlanFile_ShouldReturnPlanSheet()
        {
            var parser = new ParsePlanActual(excelFilePath);
            Assert.That(parser.ParsePlanSheet() != null);
        }
        //CHAPTER 06 - Navigation Aids
        [Test]
        public void ParsePlanSheet_ForPlanFile_ShouldReturnActualSheetName()
        {
            var parser = new ParsePlanActual(excelFilePath);
            Assert.AreEqual(ParsePlanActual.ActualSheetName, parser.ParsePlanSheet().Name);
        }

        [Test]
        public void ParsePlanSheet_ForPlanFile_ShouldReturnActualSubjects()
        {
            var parser = new ParsePlanActual(excelFilePath);
            Assert.That(parser.ParsePlanSheet().PlanItems.Any(x => x.Subject == "CHAPTER 06 - Navigation Aids"));
        }
    }
}
