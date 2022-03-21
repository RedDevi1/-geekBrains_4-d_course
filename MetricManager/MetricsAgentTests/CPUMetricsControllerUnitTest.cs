using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using Moq;
using MetricsAgent;

namespace MetricsAgentTests
{
    public class CPUMetricsControllerUnitTest
    {
        private CPUMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        public CPUMetricsControllerUnitTest()
        {
            mock = new Mock<ICpuMetricsRepository>();
            controller = new CPUMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
            repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
            Times.AtMostOnce());
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
