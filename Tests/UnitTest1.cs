using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System;
    using System.IO;

    using StatsEngine;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class XmlParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var xmlParser = new EventParser();

            string fileContent =
                File.ReadAllText(
                    @"C:\Users\rune.rystad\Documents\Visual Studio 2010\Projects\EventorStats\Tests\Data\Downloaded.xml");
            var result = xmlParser.Parse(fileContent);

            Assert.IsNotNull(result);
        }
     
        [TestMethod]
        public void ParseOrganisations()
        {
            var xmlParser = new OrgParser();

            string fileContent =
                File.ReadAllText(
                    @"C:\Users\rune.rystad\Documents\Visual Studio 2010\Projects\EventorStats\Tests\Data\Organisations.xml");

            var result = xmlParser.Parse(fileContent);

            var nsk = (from o in result where o.Id == "245" select o).First();

            Assert.AreEqual("3", nsk.ParentId);
            Assert.AreEqual("Akershus og Oslo", nsk.ParentName);
        }

        [TestMethod]
        public void DownloadEvents()
        {
            var proxy = new EventorWebService("apikey", string.Empty);

            var events = proxy.GetEvents(new DateTime(2011, 01, 01), new DateTime(2012, 01, 01));

            Assert.IsNotNull(events);
        }

        [TestMethod]
        public void DownloadOrganisations()
        {
            var proxy = new EventorWebService("apikey", string.Empty);

            var response = proxy.GetAllOrganisations();

            Assert.IsNotNull(response);
        }
    }
}