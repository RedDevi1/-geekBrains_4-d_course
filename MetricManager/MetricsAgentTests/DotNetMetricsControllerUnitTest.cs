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
    public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private readonly Mock<ILogger<DotNetMetricsController>> mockLogger;
        public DotNetMetricsControllerUnitTest()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит DotnetMetric - объект
            mock.Setup(repository =>
            repository.Create(It.IsAny<DotnetMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.Requests.DotnetMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<DotnetMetric>()),
            Times.AtMostOnce());
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
}
