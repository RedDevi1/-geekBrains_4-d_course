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
    public class NetworkMetricsControllerUnitTest
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private readonly Mock<ILogger<NetworkMetricsController>> mockLogger;
        public NetworkMetricsControllerUnitTest()
        {
            mock = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит NetworkMetric - объект
            mock.Setup(repository =>
            repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.Requests.NetworkMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()),
            Times.AtMostOnce());
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
}
