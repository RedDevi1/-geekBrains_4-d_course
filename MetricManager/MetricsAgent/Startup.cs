using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsAgent.DAL;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // Задаём новый текст команды для выполнения
                // Удаляем все таблицы с метриками, если они есть в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // Отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10, 12.01:00:00)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(15, 15.12:12:00)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(100, 03.03:05:03)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(34, 27.11:05:03)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(14, 17.09:09:12)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(240, 07.01:17:34)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(234, 01.01:05:03)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(26, 21.09:21:21)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(99, 30.10:37:33)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(233, 03.01:23:56)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(343, 14.07:22:11)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(165, 15.02:37:33)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(32, 03.03:14:39)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(45, 17.05:22:34)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(125, 01.01:45:59)";
                command.ExecuteNonQuery();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
