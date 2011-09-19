namespace StatsEngine
{
    using System.Collections.Generic;

    public class OrgFiller
    {
        private readonly List<Org> orgs;

        public OrgFiller(List<Org> orgs)
        {
            this.orgs = orgs;
        }

        public void FillOrgNames(List<FileRow> rows)
        {
            rows.ForEach(LookUpOrgInfo);
        }

        private void LookUpOrgInfo(FileRow fileRow)
        {
            var org = orgs.Find(o => o.Id == fileRow.OrgId);

            fileRow.OrgName = org.Name;
            fileRow.ParentOrgName = org.ParentName;
        }
    }
}
