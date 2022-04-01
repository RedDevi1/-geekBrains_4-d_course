using System;
using MetricsManager.Requests;
using MetricsManager.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    interface IMetricsAgentClient
    {
        public interface IMetricsAgentClient
        {
            AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
            AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
            AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetricsApiRequest request);
            AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
        }
    }
}
