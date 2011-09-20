namespace StatsEngine
{
    using System;

    public class AggregateService
    {        
        private readonly EventRepository eventRepository;
        private readonly OrganisationRepository orgRepository;
        private readonly ExcelWriter excelWriter;

        public AggregateService(EventRepository eventRepository, OrganisationRepository orgRepository, ExcelWriter excelWriter)
        {
            this.eventRepository = eventRepository;
            this.orgRepository = orgRepository;
            this.excelWriter = excelWriter;
        }

        public void CreateExcelFile(string excelFileName, DateTime fromDate)
        {                        
            var fileRows = eventRepository.GetAllEventsForNextYear(fromDate);
            
            orgRepository.FillOrgNames(fileRows);
           
            excelWriter.Write(fileRows, excelFileName);
        }
    }
}
