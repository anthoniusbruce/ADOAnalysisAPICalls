using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ADOAnalysisAPICalls
{
    public class AdoAnalysisClient : IAdoAnalysisClient
    {
        private const string Token = "";
        private const string AdoAnalysisBaseAddress = "https://analytics.dev.azure.com/tr-tax/TaxProf/_odata/v4.0-preview/";
        private const string CycleTimeScatterPlotParameters = "WorkItems?%24select=ActivatedDate%2C+CompletedDate%2C+WorkItemId&%24filter=%28WorkItemType+eq+%27{0}%27%29+and+StateCategory+eq+%27Completed%27+and+CompletedDateSK+ge+{1}+and+startswith%28Area%2FAreaPath%2C%27{2}%27%29";
        private const string UserStoryThroughputParameters = "WorkItems?%24apply=filter%28%28WorkItemType+eq+%27User+Story%27+or+WorkItemType+eq+%27Bug%27%29+and+StateCategory+eq+%27Completed%27+and+CompletedDateSK+ge+{0}+and+Teams%2Fany%28t%3At%2FTeamName+eq+%27{1}%27%29%29+%2Fgroupby%28%28CompletedDateSK%29%2Caggregate%28%24count+as+Throughput%29%29";
        private const string FeatureThroughputParameters = "WorkItems?%24apply=filter%28%28WorkItemType+eq+%27Feature%27%29+and+StateCategory+eq+%27Completed%27+and+CompletedDateSK+ge+{0}+and+Teams%2Fany%28t%3Astartswith%28t%2FTeamName%2C+%27{1}%27%29%29%29+%2Fgroupby%28%28CompletedDateSK%29%2Caggregate%28%24count+as+Throughput%29%29";

        public async Task<ScatterPlotResponse?> GetUserStoryCycleTimeScatterPlotData(DateTime date,
            string adoTeam)
        {
            return await GetCycleTimeScatterPlotData("User Story", date, adoTeam);
        }

        public async Task<ScatterPlotResponse?> GetFeatureCycleTimeScatterPlotData(DateTime date, string adoTeam)
        {
            return await GetCycleTimeScatterPlotData("Feature", date, adoTeam);
        }

        private async Task<ScatterPlotResponse?> GetCycleTimeScatterPlotData(string workItemType, DateTime date, string adoTeam)
        {
            int completedDate = FormatSkDate(date);
            string team = FormatOdata(adoTeam);

            HttpClient httpClient = CreateHttpClient();

            return
                await httpClient.GetFromJsonAsync<ScatterPlotResponse>(string.Format(CycleTimeScatterPlotParameters, workItemType, completedDate, team));
        }

        public async Task<ThroughputResponse?> GetUserStoryThroughputData(DateTime date, string adoTeam)
        {
            return await GetThroughputData(UserStoryThroughputParameters, date, adoTeam);
        }

        public async Task<ThroughputResponse?> GetFeatureThroughputData(DateTime date, string adoTeam)
        {
            return await GetThroughputData(FeatureThroughputParameters, date, adoTeam);
        }

        private async Task<ThroughputResponse?> GetThroughputData(string parameters, DateTime date, string adoTeam)
        {
            var completedDate = FormatSkDate(date);
            var team = FormatOdata(adoTeam);

            var httpClient = CreateHttpClient();

            return await httpClient.GetFromJsonAsync<ThroughputResponse>(string.Format(parameters, completedDate, team));
        }

        private static string FormatOdata(string odataOriginal)
        {
            var odata = odataOriginal.Replace("/", "%5C");
            odata = odata.Replace("(", "%28");
            odata = odata.Replace(")", "%29");
            odata = odata.Replace("'", "%27");
            odata = odata.Replace(" ", "+");

            return odata;
        }

        private static int FormatSkDate(DateTime date)
        {
            return date.Year * 10000 + date.Month * 100 + date.Day;
        }

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(AdoAnalysisBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", Token))));
            return httpClient;
        }
    }
}

