using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Requests;
using MetricsManager.Responses;
using MetricsAgent.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public MetricsAgentClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/hdd/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/ram/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/dotnet/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
