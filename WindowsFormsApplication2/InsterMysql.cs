using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApplication2.TencentVmsInterface;

namespace WindowsFormsApplication2
{
    class InsterMysql
    {
        public Boolean insterInfoToMysqlTencentVmsLog(CallBackResult zb, string Params, string mobile)
        {
            Boolean fg;
            String connetStr = "server=10.129.27.1;port=3306;user=;password=; database=;";

            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                //在这里使用代码对数据库进行增删查改
                string insertIntoStr = @"INSERT INTO `Monitor`.`VMS_LOG` (`callid`, `errmsg`, `mobile`, `message`, `datetime`, `callResult`, `keypress`, `CallBackResult`, `accept_time`) VALUES ('" + zb.callid + "', '" + zb.errmsg + "', '" + mobile + "', '" + Params + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '', '', '', '');";
                MySqlCommand cmd = new MySqlCommand(insertIntoStr, conn);
                int result = cmd.ExecuteNonQuery();
                fg= true;
            }
            catch (MySqlException ex)
            {
                // Console.WriteLine(ex.Message);
                fg= false;
            }
            finally
            {
                conn.Close();
            }
            return fg;
        }
        public Boolean insterIntoEVENT_LOG(string filetime,string datetime,string fileType,string eventMsg,string absentStation) {
            Boolean fg;
            String connetStr = "server=10.129.27.1;port=3306;user=*;password=*; database=Monitor;";

            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                //在这里使用代码对数据库进行增删查改
                string insertIntoStr = @"INSERT INTO `Monitor`.`EVENT_LOG` (`id`,`filetime`, `datetime`, `filetype`, `event_msg`, `absent_station`, `other`) VALUES (NULL, '" + filetime + "', '" + datetime+"', '"+fileType+"', '"+eventMsg+"', '"+absentStation+"', NULL);";
                
                
                MySqlCommand cmd = new MySqlCommand(insertIntoStr, conn);
                int result = cmd.ExecuteNonQuery();
                fg = true;
            }
            catch (MySqlException ex)
            {
                // Console.WriteLine(ex.Message);
                fg = false;
            }
            finally
            {
                conn.Close();
            }
            return fg;
        }
        public Boolean insterInfoToMysqlSmsLog(string msg, string mobile,string status)
        {
            DateTime dt = DateTime.Now;
            Boolean fg;
            String connetStr = "server=10.129.27.1;port=3306;user=*;password=*; database=Monitor;";

            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                //在这里使用代码对数据库进行增删查改
                string insertIntoStr = @"INSERT INTO `Monitor`.`SMS_LOG` (`eventID`, `enentMsg`, `datetime`, `mobile`, `sensResult`) VALUES (NULL, '"+msg+"', '"+dt.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+ mobile + "', '"+status+"');";
                MySqlCommand cmd = new MySqlCommand(insertIntoStr, conn);
                int result = cmd.ExecuteNonQuery();
                fg = true;
            }
            catch (MySqlException ex)
            {
                // Console.WriteLine(ex.Message);
                fg = false;
            }
            finally
            {
                conn.Close();
            }
            return fg;
        }
    }
}

