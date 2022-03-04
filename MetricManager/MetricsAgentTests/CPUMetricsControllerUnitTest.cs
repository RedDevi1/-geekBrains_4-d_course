using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class CPUMetricsControllerUnitTest
    {
        private CPUMetricsController controller;
        public CPUMetricsControllerUnitTest()
        {
            controller = new CPUMetricsController();
        }

        [Fact]
        public void GetMetricsWithPercentiles_ReturnsOk()
        {
            var percentile = 30;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsWithPercentiles(fromTime, toTime, percentile);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsWithoutPercentiles_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsWithoutPercentiles(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        public DotNetMetricsControllerUnitTest()
        {
            controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetErrorsCount_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetErrorsCount(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

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

    public class NetworkMetricsControllerUnitTest
    {
        private NetworkMetricsController controller;
        public NetworkMetricsControllerUnitTest()
        {
            controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsFromTimeToTime_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

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
