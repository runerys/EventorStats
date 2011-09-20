using System.Linq;

namespace StatsEngine
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class OrganisationParser
    {
        private List<Org> orgs;

        public IEnumerable<Org> Orgs
        {
            get
            {
                return orgs;
            }
        }

        public void Load(string xmlString)
        {
            xmlString = xmlString.Replace("&", "");

            var xdoc = XDocument.Parse(xmlString);

            var allOrgs = from e in xdoc.Descendants("Organisation")                         
                          select new
                              {
                                  Id = e.Element("OrganisationId").Value,
                                  Name = e.Element("Name").Value,
                                  ParentId = e.Element("ParentOrganisation") == null ? string.Empty : e.Element("ParentOrganisation").Element("OrganisationId").Value
                              };

            var orgs = from org in allOrgs
                       join parent in allOrgs on org.ParentId equals parent.Id
                       select new Org
                           {
                               Id = org.Id,
                               Name = org.Name,
                               ParentId = org.ParentId,
                               ParentName = parent.Name
                           };

            this.orgs = orgs.ToList();
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
