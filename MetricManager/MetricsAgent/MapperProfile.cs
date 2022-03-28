using System;
using AutoMapper;
using MetricsAgent.Metrics;
using MetricsAgent.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Добавлять сопоставления в таком стиле надо для всех объектов
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotnetMetric, DotnetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}
