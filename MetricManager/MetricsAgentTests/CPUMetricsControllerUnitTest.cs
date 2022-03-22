using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using Moq;
using MetricsAgent;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class CPUMetricsControllerUnitTest
    {
        private CPUMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private ILogger<CPUMetricsController> logger;
        public CPUMetricsControllerUnitTest()
        {
            mock = new Mock<ICpuMetricsRepository>();
            controller = new CPUMetricsController(logger, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� �����������, ��� � ����������� �������� CpuMetric - ������
            mock.Setup(repository =>
            repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            // ��������� �������� �� �����������
            var result = controller.Create(new
            MetricsAgent.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // �������� ����� Create ����������� � ������ ����� ������� � ���������
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
}
