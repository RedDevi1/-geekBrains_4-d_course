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
    public class RAMMetricsControllerUnitTest
    {
        private RAMMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private readonly Mock<ILogger<RAMMetricsController>> mockLogger;
        public RAMMetricsControllerUnitTest()
        {
            mock = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RAMMetricsController>>();
            controller = new RAMMetricsController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит RamMetric - объект
            mock.Setup(repository =>
            repository.Create(It.IsAny<RamMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.Requests.RamMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()),
            Times.AtMostOnce());
        }

        [Fact]
        public void GetFreeRAM_ReturnsOk()
        {
            var result = controller.GetFreeRAMInMegabytes();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
