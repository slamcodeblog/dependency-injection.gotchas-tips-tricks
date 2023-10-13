namespace SlamCodeBlog.DotNetDITips.Services
{
    public class TimeConsumingService
    {
        private readonly GetDateTime getDateTime;

        public TimeConsumingService(GetDateTime getDateTime)
        {
            this.getDateTime = getDateTime;
        }
    }
}
