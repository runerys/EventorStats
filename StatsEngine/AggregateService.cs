namespace StatsEngine
{
    using System;

    public class AggregateService
    {
        private readonly EventorWebService webService;
        private readonly EventParser eventParser;
        private readonly OrganisationParser orgParser;
        private readonly ExcelWriter writer;

        public AggregateService(EventorWebService webService, EventParser eventParser, OrganisationParser orgParser, ExcelWriter writer)
        {
            this.webService = webService;
            this.eventParser = eventParser;
            this.orgParser = orgParser;
            this.writer = writer;
        }

        public void CreateExcelFile(string excelFileName, DateTime fromDate)
        {                        
            var toDate = fromDate.AddYears(1);

            string eventsXml = webService.GetEvents(fromDate, toDate);
            string orgXml = webService.GetAllOrganisations();

            var fileRows = eventParser.Parse(eventsXml);
            orgParser.Load(orgXml);

            orgParser.FillOrgNames(fileRows);
           

            writer.Write(fileRows, excelFileName);
        }
    }
}
