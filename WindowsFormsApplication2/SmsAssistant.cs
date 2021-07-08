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
using static WindowsFormsApplication2.getStationInfo;

namespace WindowsFormsApplication2
{
    public partial class SmsAssistant : Form
    {
        public SmsAssistant()
        {
            InitializeComponent();
        }

        private void SmsAssistant_Load(object sender, EventArgs e)
        {
           // Button[] bu = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };


            ttt();
        }
        public void ttt() {
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
                String sqlSelect = "SELECT * FROM `STATION_INFO`";
                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand2 = new MySqlCommand(sqlSelect, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                // 执行MySQL语句，接收查询到的MySQL结果
                MySqlDataReader mdr = mySqlCommand2.ExecuteReader();
                // 读取数据
                int i = 0;
                int poinx = 0;
                int poiny = 0;
                while (mdr.Read())
                { 
                    if (poiny > 200) {
                        poinx = poinx+105;
                        poiny = 0; 
                    }
                    addCheck(mdr.GetString("StationName"), poinx, poiny ,i, mdr.GetString("Station_ID"));
                    poiny = poiny + 20;
                    i++;

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

        }
       
        //CheckBox chbox = new CheckBox();
        CheckBox[] btns = new CheckBox[81];
        int y = 0;
        public void addCheck(string name,int poinx,int poiny,int i,string id) {
            

           
         
                btns[i] = new CheckBox(); //这一句往往为初学者忽视，须知要创建对象的实例！
                
                btns[i].Location = new Point(poinx, poiny);
                btns[i].Name = id;
                //btns[i].Size = new System.Drawing.Size(4, 4);
                btns[i].Text = name;
                btns[i].Checked = true;
                //btns[i].Click = new System.EventHandler(this.btns_Click); //统一的事件处理
                this.Controls.Add(btns[i]); //在窗体上呈现控件
            
        }
        public void sendAllStation(string msg) {

            if (MessageBox.Show("确定要发送吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
            {
                try
                {
                    for (int i = 0; i < 81; i++)
                    {
                        if (btns[i].Checked) {
                            getStationInfo getPhone = new getStationInfo();
                            StationInfo allPhome = getPhone.getStationInfoByStationID(btns[i].Name, "国家站");
                            SMS sendSmsbyStation = new SMS();
                            //string smsmsg ="测试";
                            sendSmsbyStation.SendSms(allPhome.DutyPhone, msg);
                        }
                    }
                }
                catch
                {

                }
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string msg = richTextBox1.Text;
            if (msg != "")
            {
                sendAllStation(msg);
                MessageBox.Show("消息发送成功!");
            }
            else {
                MessageBox.Show("消息为空!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i =0;i<btns.Count();i++) {
                btns[i].Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < btns.Count(); i++)
            {
                btns[i].Checked = true;
            }
        }
    }
}
