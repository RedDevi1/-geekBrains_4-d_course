using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Requests
{
    public class GetAllDotNetMetricsApiRequest
    {
        public Uri ClientBaseAddress { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
