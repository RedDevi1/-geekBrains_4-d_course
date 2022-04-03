using System;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        // Счётчик для метрики HDD
        private PerformanceCounter _hddCounter;
        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости HDD
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            // Узнаем, когда мы сняли значение метрики
            var time = DateTime.UtcNow;

            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Metrics.HddMetric { Time = time, Value = hddUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
