using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string passwd = textBox2.Text;
            if (loginByUser(username, passwd))
            {
                //MessageBox.Show("successful");
                SmsAssistant d = new SmsAssistant();
                d.Show();
                this.Close();
            }
            else {
                MessageBox.Show("登录失败,请联系管理员!");
            }
        }
        public bool loginByUser(string username,string passwd) {
            bool fg = false;
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Database = "Monitor";   // 设置连接的数据库名
            mysqlCSB.Server = "10.129.27.1";  // 设置连接数据库的IP地址
            mysqlCSB.Port = 3306;           // MySql端口号
<<<<<<< HEAD
            mysqlCSB.UserID = "*";       // 设置登录数据库的账号
            mysqlCSB.Password = "*";     // 设置登录数据库的密码
=======
            mysqlCSB.UserID = "Monitor";       // 设置登录数据库的账号
            mysqlCSB.Password = "Txk@268";     // 设置登录数据库的密码
>>>>>>> f73748138f825c1c55d370238f6f87af227f6b6f
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
                String sqlSelect = @"SELECT * FROM `USER` WHERE loginname='"+ username + "'And passwd='"+ passwd + "'";
                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand2 = new MySqlCommand(sqlSelect, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                // 执行MySQL语句，接收查询到的MySQL结果
                MySqlDataReader mdr = mySqlCommand2.ExecuteReader();
                // 读取数据
                if (mdr.HasRows) {
                    fg = true;
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

            return fg;
        }
    }
}
<<<<<<< HEAD

=======
>>>>>>> f73748138f825c1c55d370238f6f87af227f6b6f
