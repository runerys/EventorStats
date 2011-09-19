using System.Linq;

namespace StatsEngine
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class OrganisationParser
    {
        public List<Org> Parse(string xmlString)
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

            return orgs.ToList();
        }
    }
}
