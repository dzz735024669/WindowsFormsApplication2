using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static WindowsFormsApplication2.getStationInfo;
using static WindowsFormsApplication2.getSubsystem_ALLConfig;
using static WindowsFormsApplication2.TencentVmsInterface;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ////cmadaasPublicInterface PBD = new cmadaasPublicInterface();
            ////PBD.cmadaasPublicDetailInterface("2021-06-09 14:00", "A.0001.0044.R001", "A.0011.0001.S001");
            ////语音消息
            //TencentVmsInterface callnew = new TencentVmsInterface();
            //string pamer = " \"合肥市 \", \"19时\"";
            //CallBackResult ddd = callnew.CallPhone("1400522308", "7226249335", "997823", pamer, "2", "13225755167");
            //InsterMysql d = new InsterMysql();
            //d.insterIntoEVENT_LOG("2021-06-20 18:00:00", "2021-06-20 18:29:00", "GNSS/MET压缩文件小时", "GNSS/MET压缩文件包缺报数大于阈值,当前到报数为:10","58321,58220,58117");
            //MessageBox.Show(ddd.errmsg.ToString());
            //getStationInfo d = new getStationInfo();
            //StationInfo stationinfo =d.getStationInfoByStationID("581009","国家站");
            //MessageBox.Show(stationinfo.StationName+ stationinfo.Station_ID+ stationinfo.DutyPhone);

            getSubsystem_ALLConfig d = new getSubsystem_ALLConfig();
            SubConfig m= d.getStationInfo();
            
            //int i = m.subsystem_config.Count;
            for (int i=0; i<=m.subsystem_config.Count-1;i++) {
                //this.dataGridView1.Rows[index].Cells[0].Value = "1";
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = m.subsystem_config[i].config_id;
                this.dataGridView1.Rows[index].Cells[1].Value = m.subsystem_config[i].data_name;
                this.dataGridView1.Rows[index].Cells[2].Value = m.subsystem_config[i].data_type;
                this.dataGridView1.Rows[index].Cells[3].Value = m.subsystem_config[i].nend_check;
                this.dataGridView1.Rows[index].Cells[4].Value = m.subsystem_config[i].sum;
                this.dataGridView1.Rows[index].Cells[5].Value = m.subsystem_config[i].absent_sum;
                this.dataGridView1.Rows[index].Cells[6].Value = m.subsystem_config[i].business_frequency;
                this.dataGridView1.Rows[index].Cells[7].Value = m.subsystem_config[i].monitor_time;
                this.dataGridView1.Rows[index].Cells[8].Value = m.subsystem_config[i].cts_code;
                this.dataGridView1.Rows[index].Cells[9].Value = m.subsystem_config[i].dpc_code;
                this.dataGridView1.Rows[index].Cells[10].Value = m.subsystem_config[i].sod_code;
                this.dataGridView1.Rows[index].Cells[11].Value = m.subsystem_config[i].sms_send;
                this.dataGridView1.Rows[index].Cells[12].Value = m.subsystem_config[i].vms_send;
                this.dataGridView1.Rows[index].Cells[13].Value = m.subsystem_config[i].all_station_info;
                //index = index + 1;
            }
           // MessageBox.Show("1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
