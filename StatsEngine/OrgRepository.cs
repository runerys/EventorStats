namespace StatsEngine
{
    using System.Collections.Generic;

    public class OrgRepository
    {
        private List<Org> orgs;

        public OrgRepository(EventorWebService webService, OrgParser parser)
        {           
            orgs = parser.Parse(webService.GetAllOrganisations());
        }      

        public virtual void PopulateOrgInfo(List<Event> rows)
        {
            rows.ForEach(LookUpOrgInfo);
        }

        private void LookUpOrgInfo(Event @event)
        {
            var org = this.orgs.Find(o => o.Id == @event.OrgId);

            @event.OrgName = org.Name;
            @event.ParentOrgName = org.ParentName;
        }
    }
}
