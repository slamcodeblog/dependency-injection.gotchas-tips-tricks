namespace SlamCodeBlog.DotNetDITips.Services
{
    public interface ITestService
    {
    }

    public class FirstTestService : ITestService
    {
        public FirstTestService(ISubService subService) { }
    }

    public class TestServiceWithNoPublicCtsr : ITestService
    {
        // at least one cstr must be public
        internal TestServiceWithNoPublicCtsr(GetDateTime getDateTime) { }
        protected TestServiceWithNoPublicCtsr(ISubService subService) { }
        private TestServiceWithNoPublicCtsr(Func<DateTime> getDateTime) { }
    }

    public class TestServiceWithMarkedCstr : ITestService
    {
        public TestServiceWithMarkedCstr(GetDateTime getDateTime, ISubService subService)
        {
            Console.WriteLine($"Constructor:(getDateTime, subService)");
        }

        // Below will be ambigous wth cstr no 1 in .Net 7 and lower
        [ActivatorUtilitiesConstructor]
        public TestServiceWithMarkedCstr(ISubService subService, TimeConsumingService timeConsumingService)
        {
            Console.WriteLine($"Constructor:(subService, timeConsumingService)");
        }

        public TestServiceWithMarkedCstr(GetDateTime getDateTime)
        {
            Console.WriteLine($"Constructor:(getDateTime)");
        }

        public TestServiceWithMarkedCstr()
        {
            Console.WriteLine($"Constructor:()");
        }
    }

    public class TestServiceWithNoDefaultCstr : ITestService
    {
        // Need to provide default cstr explicitly, implicit one is not taken into account when any other cstr is defined
        public TestServiceWithNoDefaultCstr(Func<string> someNotRegisteredFunc)
        {
            Console.WriteLine($"Constructor:(someNotRegisteredFunc)");
        }

        //public TestServiceWithNoDefaultCstr()
        //{
        //    Console.WriteLine($"Constructor:()");
        //}
    }

    internal class TestServiceWithNoCstr : ITestService
    {
        // Implicit default cstr is used only if there is no other public one defined
    }

    public class TestServiceWithDiffNoOfParamsInCstr : ITestService
    {
        public TestServiceWithDiffNoOfParamsInCstr()
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:()");
        }

        public TestServiceWithDiffNoOfParamsInCstr(GetDateTime getDateTime)
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:(getDateTime)");
        }

        public TestServiceWithDiffNoOfParamsInCstr(ISubService subService, TimeConsumingService timeConsumingService)
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:(subService, timeConsumingService)");
        }

        public TestServiceWithDiffNoOfParamsInCstr(GetDateTime getDateTime, ISubService subService, TimeConsumingService timeConsumingService)
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:(getDateTime, subService, timeConsumingService)");
        }

        // This cnstructor will be sued as it has most resolvable parameters
        public TestServiceWithDiffNoOfParamsInCstr(GetDateTime getDateTime, ISubService subService, TimeConsumingService timeConsumingService, Func<DateTime> getDataTimeFunc)
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:(getDateTime, subService, timeConsumingService, getDataTimeFunc)");
        }

        public TestServiceWithDiffNoOfParamsInCstr(GetDateTime getDateTime, ISubService subService, TimeConsumingService timeConsumingService, Func<DateTime> getDataTimeFunc, Func<string> someStringFunc)
        {
            Console.WriteLine($"{nameof(TestServiceWithDiffNoOfParamsInCstr)} Constructor:(getDateTime, subService, timeConsumingService, getDataTimeFunc, someStringFunc)");
        }
    }
}
