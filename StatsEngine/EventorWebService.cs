
namespace StatsEngine
{
    using System;
    using System.Net;
    using System.Text;

    public class EventorWebService
    {      
        public EventorWebService()
        {
            BaseUrl = "https://eventor.orientering.no/api/";
        }

        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }

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
            if (string.IsNullOrEmpty(ApiKey))
                throw new InvalidOperationException("Missing ApiKey for Http Header");

            var client = new WebClient();
            client.Headers.Add("ApiKey", ApiKey);

            string url = string.Format("{0}{1}", BaseUrl, query);

            var bytes = client.DownloadData(url);
            var response = Encoding.UTF8.GetString(bytes);
            return response;
        }
    }
}
