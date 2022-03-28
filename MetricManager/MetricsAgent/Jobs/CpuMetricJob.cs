using System;
using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Теперь можно записать что-то посредством репозитория
            return Task.CompletedTask;
        }
    }
}
