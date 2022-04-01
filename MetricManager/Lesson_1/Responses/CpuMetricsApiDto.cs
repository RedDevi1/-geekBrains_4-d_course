using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class CpuMetricsApiDto
    {
        public Uri ClientBaseAddress { get; set; }
        public List<CpuMetricsApiDto> Metrics { get; set; }
    }
}
