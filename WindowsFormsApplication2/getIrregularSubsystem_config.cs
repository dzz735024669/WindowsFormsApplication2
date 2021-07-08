using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class getIrregularSubsystem_config
    {
        public class irregularAllConfig
        {
            public List<irregularSubsystem_ALLConfig> date { get; set; }
        }
        public class irregularSubsystem_ALLConfig
        {
            public string config_id { get; set; }
            public string cts_code { get; set; }
            public string sod_code { get; set; }
            public string dpc_code { get; set; }
            public string data_name { get; set; }
            public string data_type { get; set; }
            public string nend_check { get; set; }
            public string business_frequency { get; set; }
            public string monitor_time { get; set; }
            public string sum { get; set; }
            public string absent_sum { get; set; }
            public string all_station_info { get; set; }
            public string sms_send { get; set; }
            public string vms_send { get; set; }
            public string send_absentSum { get; set; }
            public string MinAdd { get; set; }
            public string HourAdd { get; set; }

        }
        public irregularAllConfig irregularDataFlowInfo()
        {
            List<irregularSubsystem_ALLConfig> irregularSubsystem_config = new List<irregularSubsystem_ALLConfig>();
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Database = "Monitor";   // 设置连接的数据库名
            mysqlCSB.Server = "10.129.27.1";  // 设置连接数据库的IP地址
            mysqlCSB.Port = 3306;           // MySql端口号
            mysqlCSB.UserID = "Monitor";       // 设置登录数据库的账号
            mysqlCSB.Password = "Txk@268";     // 设置登录数据库的密码
            //string mysqlCSB = "Database=school;Data Source=127.0.0.1;port=3306;User Id=root;Password=yang;";
            // 创建连接
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCSB.ToString());
            // 打开连接(如果处于关闭状态才进行打开)
            if (mySqlConnection.State == ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }
            // 对应数据库里面的表字段
            try
            {

                // 打开连接(如果处于关闭状态才进行打开)
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }
                // 创建要查询的MySQL语句
                String sqlSelect = "SELECT * FROM `subsystem_allconfig_irregular`";
                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand2 = new MySqlCommand(sqlSelect, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                // 执行MySQL语句，接收查询到的MySQL结果
                MySqlDataReader mdr = mySqlCommand2.ExecuteReader();
                // 读取数据
                int i = 0;
                while (mdr.Read())
                {
                    irregularSubsystem_ALLConfig subsystem_allonfig = new irregularSubsystem_ALLConfig();
                    subsystem_allonfig.config_id = mdr.GetString("config_id");
                    subsystem_allonfig.cts_code = mdr.GetString("cts_code");
                    subsystem_allonfig.sod_code = mdr.GetString("sod_code");
                    subsystem_allonfig.dpc_code = mdr.GetString("dpc_code");
                    subsystem_allonfig.data_name = mdr.GetString("data_name");
                    subsystem_allonfig.data_type = mdr.GetString("data_type");
                    subsystem_allonfig.nend_check = mdr.GetString("nend_check");
                    subsystem_allonfig.business_frequency = mdr.GetString("business_frequency");
                    subsystem_allonfig.monitor_time = mdr.GetString("monitor_time");
                    subsystem_allonfig.sum = mdr.GetString("sum");
                    subsystem_allonfig.absent_sum = mdr.GetString("absent_sum");
                    subsystem_allonfig.all_station_info = mdr.GetString("all_station_info");
                    subsystem_allonfig.sms_send = mdr.GetString("sms_send");
                    subsystem_allonfig.vms_send = mdr.GetString("vms_send");
                    subsystem_allonfig.send_absentSum = mdr.GetString("send_absentSm");
                    subsystem_allonfig.MinAdd = mdr.GetString("MinAdd");
                    subsystem_allonfig.HourAdd = mdr.GetString("HourAdd");
                    ;

                    irregularSubsystem_config.Add(subsystem_allonfig);


                }
            }
            catch (Exception)
            {
                string m1m = "1";
            }
            finally
            {
                // 关闭连接
                mySqlConnection.Close();
            }
            irregularAllConfig allIrregularConfid = new irregularAllConfig();
            allIrregularConfid.date = irregularSubsystem_config;
            return allIrregularConfid;
        }
    }
}
