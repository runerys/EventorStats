
namespace StatsEngine
{
    using FileHelpers;

    [DelimitedRecord(";")]
    public class FileRow
    {
        public string EventId;
        public string Date;
        public string Month;
        public string Name;
        public string OrgId;
        public string OrgName;
        public string ParentOrgName;
        public int HasResults;             
        public int HasStartList;
        public int HasEntries;
    }
}
