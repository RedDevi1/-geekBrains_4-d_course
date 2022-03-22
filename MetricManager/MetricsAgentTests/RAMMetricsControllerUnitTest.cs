using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class RAMMetricsControllerUnitTest
    {
        private RAMMetricsController controller;
        public RAMMetricsControllerUnitTest()
        {
            controller = new RAMMetricsController();
        }

        [Fact]
        public void GetFreeRAM_ReturnsOk()
        {
            var result = controller.GetFreeRAMInMegabytes();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
