using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotnetMetric>
    {
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор
        public DotNetMetricsRepository()
        {
            // Добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        public void Create(DotnetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
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
                connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }
        public void Update(DotnetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }
        public IList<DotnetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Читаем, используя Query, и в шаблон подставляем тип данных,
                // объект которого Dapper, он сам заполнит его поля
                // в соответствии с названиями колонок
                return connection.Query<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
            }
        }
        public DotnetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
                    new
                    {
                        id = id
                    });
            }
        }
        public IList<DotnetMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotnetMetric>("SELECT * FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime").ToList();
            }
        }
    }
}
