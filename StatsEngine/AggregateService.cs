namespace StatsEngine
{
    using System;

    public class AggregateService
    {
        public void CreateExcelFile()
        {
            string excelFileName = @"c:\temp\eventor.xls";

            var fromDate = new DateTime(2011, 01, 01);
            var toDate = fromDate.AddYears(1);

            var proxy = new EventorWebService();
            string eventsXml = proxy.GetEvents(fromDate, toDate);
            string orgXml = proxy.GetAllOrganisations();

            var fileRows = new EventParser().Parse(eventsXml);

            var orgs = new OrganisationParser().Parse(orgXml);

            new OrgFiller(orgs).FillOrgNames(fileRows);

            new ExcelWriter().Write(fileRows, excelFileName);
        }
    }
}
