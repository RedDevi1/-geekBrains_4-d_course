using Core.Interfaces;
using MetricsAgent.Metrics;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotnetMetric>
    {
    }
}
