using System;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotnetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        // Счётчик для метрики gc-heap-size
        private PerformanceCounter _dotnetCounter;
        public DotnetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter();
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости gc-heap-size
            var dotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());

            // Узнаем, когда мы сняли значение метрики
            var time = DateTime.UtcNow;

            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Metrics.DotnetMetric { Time = time, Value = dotnetUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
