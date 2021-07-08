using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class getStationInfo
    {
        public class StationInfo
        {
            public string Station_ID { get; set; }
            public string StationName { get; set; }
            public string BusinessLeader { get; set; }
            public string LeaderPhone { get; set; }
            public string DutyTelephone { get; set; }
            public string DutyPhone { get; set; }
            public string DutyTelephone1 { get; set; }
            public string DutyPhone2 { get; set; }
            public string DutyPhone3 { get; set; }
            public string businessBossName { get; set; }
            public string businessBossPhone { get; set; }
            public string filetype { get; set; }
        }
        public StationInfo getStationInfoByStationID(string stationId,string filetype) {    
            StationInfo station = new StationInfo();
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
                String sqlSelect = "SELECT * from STATION_INFO where Station_ID='"+stationId+"'and filetype='"+filetype+"'";
                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand2 = new MySqlCommand(sqlSelect, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                // 执行MySQL语句，接收查询到的MySQL结果
                MySqlDataReader mdr = mySqlCommand2.ExecuteReader();
                // 读取数据
                while (mdr.Read())
                {
                    station.StationName = mdr.GetString("StationName");
                    station.Station_ID = mdr.GetString("Station_ID");
                    station.BusinessLeader = mdr.GetString("BusinessLeader");
                    station.LeaderPhone = mdr.GetString("LeaderPhone");
                    station.DutyTelephone = mdr.GetString("DutyTelephone");
                    station.DutyPhone = mdr.GetString("DutyPhone");
                    station.DutyTelephone1 = mdr.GetString("DutyTelephone1");
                    station.DutyPhone2 = mdr.GetString("DutyPhone2");
                    station.DutyPhone3 = mdr.GetString("DutyPhone3");
                    station.businessBossName = mdr.GetString("businessBossName");
                    station.businessBossPhone = mdr.GetString("businessBossPhone");
                    station.filetype = mdr.GetString("filetype");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
               // 关闭连接
                mySqlConnection.Close();
            }

            return station;
        }
    }
}
