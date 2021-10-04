using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MOTCheck.Controller
{
    public static class GovUKServiceAdaptor
    {
        private static readonly HttpClient _httpClient;

        static GovUKServiceAdaptor()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json+v6"));
            //TODO AppSettings / secrets?
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");
            //_httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            //{ 
            //    NoCache = true,
            //    NoStore = true
            //};
            _httpClient.Timeout = new TimeSpan(hours: 0, minutes: 0, seconds: 10);
            //https://dvsa.github.io/mot-history-api-documentation/
        }

        public static bool GetMOTTests(string a_sRegistration, out string a_sResponseContent)
        {
            bool bOK = false;

            string sEndPoint = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=" + a_sRegistration;

            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(sEndPoint).ConfigureAwait(false).GetAwaiter().GetResult();
            a_sResponseContent = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK) bOK = true;

            return bOK;
        }
    }
}