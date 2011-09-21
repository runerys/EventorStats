namespace StatsEngine
{
    using System;

    public class AggregateService
    {        
        private readonly EventRepository eventRepository;
        private readonly OrgRepository orgRepository;
        private readonly ExcelWriter excelWriter;

        public AggregateService(EventRepository eventRepository, OrgRepository orgRepository, ExcelWriter excelWriter)
        {
            this.eventRepository = eventRepository;
            this.orgRepository = orgRepository;
            this.excelWriter = excelWriter;
        }

        public void CreateExcelFile(string excelFileName, DateTime fromDate)
        {                        
            var fileRows = eventRepository.GetAllEventsForNextYear(fromDate);
            
            orgRepository.PopulateOrgInfo(fileRows);
           
            excelWriter.Write(fileRows, excelFileName);
        }
    }
}
