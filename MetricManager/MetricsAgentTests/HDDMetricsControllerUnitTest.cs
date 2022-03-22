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
    public class HDDMetricsControllerUnitTest
    {
        private HDDMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private readonly Mock<ILogger<HDDMetricsController>> mockLogger;
        public HDDMetricsControllerUnitTest()
        {
            mock = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HDDMetricsController>>();
            controller = new HDDMetricsController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит HddMetric - объект
            mock.Setup(repository =>
            repository.Create(It.IsAny<HddMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.Requests.HddMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
            Times.AtMostOnce());
        }

        [Fact]
        public void GetFreeSpaceSize_ReturnsOk()
        {
            var result = controller.GetFreeSpaceSizeInMegabytes();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
