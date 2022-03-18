using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsWithPercentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int percentile)
        {
            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsWithoutPercentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source = :memory:";
            string stm = "SELECT SQLITE_VERSION()";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();

                return Ok(version);
            }
        }
        [HttpGet("sql-read-write-test")]
        public IActionResult TryToInsertAndRead()
        {
            string cs = "Data Source = :memory:";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(con);
                {
                    cmd.CommandText = "drop table if exists cpumetrics";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"create table cpumetrics(id integer primary key, value int, time int)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into cpumetrics(value, time) values(10, 1)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into cpumetrics(value, time) values(50, 2)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into cpumetrics(value, time) values(75, 4)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into cpumetrics(value, time) values(90, 5)";
                    cmd.ExecuteNonQuery();

                    string readQuery = "select * from cpumetrics limit 3";
                    var returnArray = new CpuMetric[3];

                    cmd.CommandText = readQuery;

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        var cnt = 0;
                        while (reader.Read())
                        {
                            returnArray[cnt] = new CpuMetric { Id = reader.GetInt32(0), Value = reader.GetInt32(1), Time = reader.GetInt64(2) };
                            cnt++;
                        }
                    }
                    return Ok(returnArray);
                }

            }
        }
    }
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
