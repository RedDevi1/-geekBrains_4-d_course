using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class HDDMetricsControllerUnitTest
    {
        private HDDMetricsController controller;
        public HDDMetricsControllerUnitTest()
        {
            controller = new HDDMetricsController();
        }

        [Fact]
        public void GetFreeSpaceSize_ReturnsOk()
        {
            var result = controller.GetFreeSpaceSizeInMegabytes();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
