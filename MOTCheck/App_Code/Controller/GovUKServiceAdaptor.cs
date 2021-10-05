using MOTCheck.Extensions;
using MOTCheck.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MOTCheck.Controller
{
    public static class GovUKServiceAdaptor
    {
        private static readonly HttpClient _httpClient;

        static GovUKServiceAdaptor()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://beta.check-mot.service.gov.uk/trade/vehicles/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json+v6"));
            //TODO AppSettings / secrets?
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true
            };
            _httpClient.Timeout = TimeSpan.FromMilliseconds(10000);
        }

        private static bool GetMotTestResponse(string a_sRegistration, out string a_sResponseContent, out string a_sErrorMessage)
        {
            a_sResponseContent = null;
            a_sErrorMessage = null;

            bool bOK = false;

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            try
            {
                string sEndPoint = "mot-tests?registration=" + a_sRegistration;

                HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(sEndPoint).ConfigureAwait(false).GetAwaiter().GetResult();
                a_sResponseContent = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    bOK = true;
                }
                else
                {
                    a_sErrorMessage = "The gov.uk request returned a " + httpResponseMessage.StatusCode.ToString("D") + " response code.";
                }
            }
            catch (WebException ex)
            {
                a_sErrorMessage = "The gov.uk request has thrown a WebException: " + (ex.Response as HttpWebResponse)?.StatusCode.ToString() ?? ex.Status.ToString().TrimTrailing(".") + ".";
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cancellationTokenSource.Token)
                {
                    a_sErrorMessage = "The gov.uk request was cancelled by MOTCheck.";
                }
                else
                {
                    a_sErrorMessage = "The gov.uk request timed out.";
                }
            }
            catch (Exception ex)
            {
                string sExceptionTypeName = ex.GetType().Name;
                a_sErrorMessage = "The gov.uk request has thrown " + sExceptionTypeName.PrefixNoun() + ".";
            }

            return bOK;
        }

        public static GovUKServiceCarModel GetMotTest(string a_sRegistration, out string a_sErrorMessage)
        {
            GovUKServiceCarModel govUKServiceCarModel = null;

            string sJson;
            if (GetMotTestResponse(a_sRegistration, out sJson, out a_sErrorMessage))
            {
                try
                {
                    govUKServiceCarModel = JsonConvert.DeserializeObject<List<GovUKServiceCarModel>>(sJson).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    string sExceptionTypeName = ex.GetType().Name;
                    a_sErrorMessage = "The MOTCheck response parser has thrown " + sExceptionTypeName.PrefixNoun() + " whilst processing the gov.uk response.";
                }
            }

            return govUKServiceCarModel;
        }
    }
}