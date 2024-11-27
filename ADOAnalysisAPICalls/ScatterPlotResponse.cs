namespace ADOAnalysisAPICalls
{
    public class ScatterPlotResponse
    {
        public List<ScatterPlot>? Value { get; set; }
    }

    public class ScatterPlot
    {
        public int WorkItemId { get; set; }
        public string? CompletedDate { get; set; }
        public string? ActivatedDate { get; set; }
    }
}