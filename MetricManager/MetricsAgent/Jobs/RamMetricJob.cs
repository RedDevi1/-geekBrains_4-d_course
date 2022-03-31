using System;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        // Счётчик для метрики RAM
        private PerformanceCounter _ramCounter;
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости RAM
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            // Узнаем, когда мы сняли значение метрики
            var time = DateTime.UtcNow;

            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Metrics.RamMetric { Time = time, Value = ramUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
