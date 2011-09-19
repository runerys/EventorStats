
namespace StatsEngine
{
    using System;
    using System.Net;
    using System.Text;

    public class EventorWebService
    {
        private string apiKey = "8ebc1e96796547518d68a8b37059e95e";
        private string baseUrl = "https://eventor.orientering.no/api/";

        public string GetEvents(DateTime from, DateTime to)
        {
            string url = string.Format("events?fromDate={0:yyyy-MM-dd}&toDate={1:yyyy-MM-dd}", from, to);

            return this.Get(url);
        }

        public string GetAllOrganisations()
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
