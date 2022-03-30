﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Metrics;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=True;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор
        public CpuMetricsRepository()
        {
            // Добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                // Анонимный объект с параметрами запроса
                new
                {
                    // Value подставится на место "@value" в строке запроса
                    // Значение запишется из поля Value объекта item
                    value = item.Value,
                    // Записываем в поле time количество секунд
                    time = item.Time.TotalSeconds
                });
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }
        public void Update(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }
        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Читаем, используя Query, и в шаблон подставляем тип данных,
                // объект которого Dapper, он сам заполнит его поля
                // в соответствии с названиями колонок
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }
        public CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id = @id",
                    new
                    {
                        id = id
                    });
            }
        }
        public IList<CpuMetric> GetByTimePeriod(DateTime fromTime, DateTime toTime)
        {
            //var metric = new CpuMetric();
            var metricsListBeforParse = new List<CpuMetric>();
            var metricsListAfterParse = new List<CpuMetric>();
            var _fromTime = fromTime.TimeOfDay;
            var _toTime = toTime.TimeOfDay;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                metricsListBeforParse = connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE time > @_fromTime AND time < @_toTime").ToList();
                foreach (var cur_metric in metricsListBeforParse)
                {
                    cur_metric.Time = 
                    metricsListAfterParse.Add(cur_metric.);
                }
            }            
        }
    }
}
