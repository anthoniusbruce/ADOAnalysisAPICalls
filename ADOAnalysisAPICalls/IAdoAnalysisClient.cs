namespace ADOAnalysisAPICalls
{
    public interface IAdoAnalysisClient
    {
        Task<ScatterPlotResponse?> GetFeatureCycleTimeScatterPlotData(DateTime date, string adoTeam);
        Task<ThroughputResponse?> GetFeatureThroughputData(DateTime date, string adoTeam);
        Task<ScatterPlotResponse?> GetUserStoryCycleTimeScatterPlotData(DateTime date, string adoTeam);
        Task<ThroughputResponse?> GetUserStoryThroughputData(DateTime date, string adoTeam);
    }
}