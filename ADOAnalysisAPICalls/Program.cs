// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using ADOAnalysisAPICalls;

var client = new AdoAnalysisClient();
var oneMonthAgo = DateTime.Now.AddMonths(-1);
var oneYearAgo = DateTime.Now.AddYears(-1);
var arAreaPath = "TaxProf/aafm/ar";
var arTeam = "ar-onvio";
var brTeam = "br-onvio-platform-tech1";

var usCtTask = client.GetUserStoryCycleTimeScatterPlotData(oneMonthAgo, arAreaPath);
var fCtTask = client.GetFeatureCycleTimeScatterPlotData(oneYearAgo, arAreaPath);
var usTpTask = client.GetUserStoryThroughputData(oneMonthAgo, brTeam);
var fTpTask = client.GetFeatureThroughputData(oneYearAgo, arTeam);

//usCtTask.Wait();
//fCtTask.Wait();
//usTpTask.Wait();
//fTpTask.Wait();

var usCtOut = JsonSerializer.Serialize(usCtTask.Result);
File.WriteAllText("usCtOut.json", usCtOut);

var fCtOut = JsonSerializer.Serialize(fCtTask.Result);
File.WriteAllText("fCtOut.json", fCtOut);

var usTpOut = JsonSerializer.Serialize(usTpTask.Result);
File.WriteAllText("usTpOut.json", usTpOut);

var fTpOut = JsonSerializer.Serialize(fTpTask.Result);
File.WriteAllText("fTpOut.json", fTpOut);

Console.WriteLine("Hello, World!");
