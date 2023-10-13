namespace SlamCodeBlog.DotNetDITips.Services
{
    public delegate DateTime GetDateTime();

    public static class DateTimeHelper
    {
        public static GetDateTime Local = () => DateTime.Now;

        public static GetDateTime Utc = () => DateTime.UtcNow;
    }
}
