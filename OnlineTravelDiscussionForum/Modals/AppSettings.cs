namespace OnlineTravelDiscussionForum.Modals
{
    public class AppSettings
    {
        public string LocalUrl { get; set; }
        public bool IsProduction { get; set; }
        public string AzureUrl { get; set; }

        public string getBaseUrl()
        {
            if (IsProduction)
            {
                return AzureUrl;
            }
            return LocalUrl;
        }
    }
}
