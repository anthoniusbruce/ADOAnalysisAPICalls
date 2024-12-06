// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using ADOAnalysisAPICalls;

var client = new AdoAnalysisClient();
var oneMonthAgo = DateTime.Now.AddMonths(-1);
var oneYearAgo = DateTime.Now.AddYears(-1);
var arAreaPath = "<complete area path>";
var team1 = "<team 1>";
var team2 = "<team 2";

var usCtTask = client.GetUserStoryCycleTimeScatterPlotData(oneMonthAgo, arAreaPath);
var fCtTask = client.GetFeatureCycleTimeScatterPlotData(oneYearAgo, arAreaPath);
var usTpTask = client.GetUserStoryThroughputData(oneMonthAgo, team2);
var fTpTask = client.GetFeatureThroughputData(oneYearAgo, team1);

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
