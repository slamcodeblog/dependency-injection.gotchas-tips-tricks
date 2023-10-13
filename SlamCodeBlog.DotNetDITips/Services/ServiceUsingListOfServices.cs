namespace SlamCodeBlog.DotNetDITips.Services
{
    public class ServiceUsingListOfServices
    {
        public ServiceUsingListOfServices(IEnumerable<ITestService> testServices)
        {
            Console.WriteLine($"{nameof(ServiceUsingListOfServices)} Constructor:({nameof(testServices)}) with {testServices.Count()} services in list");
        }
    }
}
