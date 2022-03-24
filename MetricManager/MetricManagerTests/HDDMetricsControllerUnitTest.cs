using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricManagerTests
{
    public class HDDMetricsControllerUnitTest
    {
        private HDDMetricsController controller;
        private readonly Mock<ILogger<HDDMetricsController>> mockLogger;
        public HDDMetricsControllerUnitTest()
        {
            mockLogger = new Mock<ILogger<HDDMetricsController>>();
            controller = new HDDMetricsController(mockLogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
