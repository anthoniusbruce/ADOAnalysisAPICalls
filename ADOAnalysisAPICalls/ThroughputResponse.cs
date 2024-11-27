namespace ADOAnalysisAPICalls
{
    public class ThroughputResponse
    {
        public List<ThroughputRec>? Value { get; set; }

    public class ThroughputRec
    {
        public int CompletedDateSK { get; set; }
        public int Throughput { get; set; }
    }
}
}