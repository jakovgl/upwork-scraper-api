namespace upwork_scraper_server.models
{
    public class Job
    {
        public string Cipher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Engagement { get; set; }
        public string ProposalTier { get; set; }
        public string Attributes { get; set; }
    }
}