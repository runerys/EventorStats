namespace StatsEngine
{
    using System;
    using System.Net;
    using System.Text;

    public class EventorWebService
    {
        private readonly string apiKey;
        private readonly string baseUrl;

        public EventorWebService(string apiKey, string baseUrl)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
        }

        public virtual string GetEvents(DateTime from, DateTime to)
        {
            string url = string.Format("events?fromDate={0:yyyy-MM-dd}&toDate={1:yyyy-MM-dd}", from, to);

            return this.Get(url);
        }

        public virtual string GetAllOrganisations()
        {
            string url = "organisations";

            return this.Get(url);
        }

        private string Get(string query)
        {
            var client = new WebClient();
            client.Headers.Add("ApiKey", apiKey);

            string url = string.Format("{0}{1}", baseUrl, query);

            var bytes = client.DownloadData(url);
            var response = Encoding.UTF8.GetString(bytes);
            return response;
        }
    }
}
