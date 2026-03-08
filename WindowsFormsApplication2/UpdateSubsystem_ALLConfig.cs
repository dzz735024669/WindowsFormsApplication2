using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApplication2.getSubsystem_ALLConfig;

namespace WindowsFormsApplication2
{
    class UpdateSubsystem_ALLConfig
    {
        public Boolean UpDatemysql(SubConfig m) {
            DateTime dt = DateTime.Now;
            Boolean fg;
            String connetStr = "server=10.129.27.1;port=3306;user=*;password=*; database=Monitor;";

            MySqlConnection conn = new MySqlConnection(connetStr);
            
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                for (int i = 0; i < m.subsystem_config.Count; i++) { 
                string updateMySqlStr = @"UPDATE `Monitor`.`subsystem_allconfig` SET `nend_check` = '"+m.subsystem_config[i].nend_check+"' WHERE `subsystem_allconfig`.`config_id` = '"+m.subsystem_config[i].config_id+"';";
                MySqlCommand cmd = new MySqlCommand(updateMySqlStr, conn);
                int result = cmd.ExecuteNonQuery();
                updateMySqlStr = @"UPDATE `Monitor`.`subsystem_allconfig` SET `sum` = '" + m.subsystem_config[i].sum + "' WHERE `subsystem_allconfig`.`config_id` = '" + m.subsystem_config[i].config_id + "';";
                cmd = new MySqlCommand(updateMySqlStr, conn);
                result = cmd.ExecuteNonQuery();
                    updateMySqlStr = @"UPDATE `Monitor`.`subsystem_allconfig` SET `absent_sum` = '" + m.subsystem_config[i].absent_sum + "' WHERE `subsystem_allconfig`.`config_id` = '" + m.subsystem_config[i].config_id + "';";
                    cmd = new MySqlCommand(updateMySqlStr, conn);
                    result = cmd.ExecuteNonQuery();
                    result = cmd.ExecuteNonQuery();
                    updateMySqlStr = @"UPDATE `Monitor`.`subsystem_allconfig` SET `monitor_time` = '" + m.subsystem_config[i].monitor_time + "' WHERE `subsystem_allconfig`.`config_id` = '" + m.subsystem_config[i].config_id + "';";
                    cmd = new MySqlCommand(updateMySqlStr, conn);
                    result = cmd.ExecuteNonQuery();

                }
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

