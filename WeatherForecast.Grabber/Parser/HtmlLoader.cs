using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecast.Grabber.Parser
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string _baseUrl;

        public HtmlLoader(string baseUrl)
        {
            client = new HttpClient();
            _baseUrl = baseUrl.Trim('/', ' ');
        }

        public async Task<string> GetSource(string prefix)
        {
            var currentUrl = _baseUrl + (string.IsNullOrWhiteSpace(prefix) ? string.Empty : '/' + prefix.Trim('/', ' '));
            var response = await client.GetAsync(currentUrl);
            string source = null;

            if(response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
