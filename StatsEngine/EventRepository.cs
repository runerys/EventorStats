namespace StatsEngine
{
    using System;
    using System.Collections.Generic;

    public class EventRepository
    {
        private readonly EventorWebService webService;

        private readonly EventParser parser;

        public EventRepository(EventorWebService webService, EventParser parser)
        {
            this.webService = webService;
            this.parser = parser;
        }

        public List<Event> GetAllEventsForNextYear(DateTime fromDate)
        {
            var xml = webService.GetEvents(fromDate, fromDate.AddYears(1));
            return parser.Parse(xml);
        }        
    }
}
