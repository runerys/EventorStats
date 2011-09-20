namespace StatsEngine
{
    using System.Collections.Generic;
    using FileHelpers.DataLink;

    public class ExcelWriter
    {
        public void Write(List<FileRow> results, string filename)
        {
            ExcelStorage storage = new ExcelStorage(typeof(FileRow));

            storage.StartColumn = 1;
            storage.StartRow = 1;
            storage.FileName = filename;

            storage.InsertRecords(results.ToArray());
        }
    }
}
