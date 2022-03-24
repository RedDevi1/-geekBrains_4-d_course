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
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10, 12)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(15, 15)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(100, 3)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(34, 27)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(14, 17)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(240, 7)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(234, 1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(26, 21)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(99, 30)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(233, 3)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(343, 14)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(165, 15)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time TimeSpan)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(32, 3)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(45, 17)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(125, 1)";
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
