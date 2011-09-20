namespace StatsEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class EventRepository
    {
        private readonly EventorWebService webService;

        public EventRepository(EventorWebService webService)
        {
            this.webService = webService;
        }

        public List<FileRow> GetAllEventsForNextYear(DateTime fromDate)
        {
            var xml = webService.GetEvents(fromDate, fromDate.AddYears(1));
            return this.Parse(xml);
        }

        private List<FileRow> Parse(string xmlString)
        {
            xmlString = xmlString.Replace("&", "");

            var xdoc = XDocument.Parse(xmlString);

            var events = from e in xdoc.Descendants("Event")
                         select new
                             {
                                 EventId = e.Element("EventId").Value,
                                 Name = e.Element("Name").Value,
                                 Date = e.Descendants("RaceDate").Elements("Date").FirstOrDefault().Value,
                                 OrganisationId = e.Element("Organiser").Element("OrganisationId").Value,
                                 EventStatusId = e.Element("EventStatusId").Value,
                                 Results = e.Descendants("HashTableEntry").Where(entry => entry.Element("Key").Value.StartsWith("officialResult_")).FirstOrDefault(),
                                 StartList = e.Descendants("HashTableEntry").Where(entry => entry.Element("Key").Value.StartsWith("startList_")).FirstOrDefault(),
                             };

            var results = from r in events
                          select new FileRow
               {
                   EventId = r.EventId,
                   Name = r.Name,
                   Date = r.Date,
                   Month = r.Date.Substring(0, 7),
                   OrgId = r.OrganisationId,
                   HasEntries = int.Parse(r.EventStatusId) >= 5 ? 1 : 0,
                   HasResults = null == r.Results ? 0 : 1,
                   HasStartList = null == r.StartList ? 0 : 1
               };

            return results.ToList();
        }
    }
}
