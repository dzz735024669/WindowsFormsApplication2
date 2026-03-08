using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static WindowsFormsApplication2.DataFlow;
using static WindowsFormsApplication2.getStationInfo;
using static WindowsFormsApplication2.getSubsystem_ALLConfig;
namespace WindowsFormsApplication2
{

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private void label8_Click(object sender, EventArgs e)
		{
		}
		private void label13_Click(object sender, EventArgs e)
		{
		}
		private void label45_Click(object sender, EventArgs e)
		{
		}
		private void label35_Click(object sender, EventArgs e)
		{
		}
		private void label20_Click(object sender, EventArgs e)
		{
		}
		private void tabPage1_Click(object sender, EventArgs e)
		{
		}
		private void panel5_Paint(object sender, PaintEventArgs e)
		{
		}
		private void label70_Click(object sender, EventArgs e)
		{
		}
		private void button3_Click(object sender, EventArgs e)
		{
		}
		private void button4_Click(object sender, EventArgs e)
		{
		}
		private void label57_Click(object sender, EventArgs e)
		{
		}
		private void label69_Click(object sender, EventArgs e)
		{
		}
		private void label67_Click(object sender, EventArgs e)
		{
		}
		private void label64_Click(object sender, EventArgs e)
		{
		}
		private void textBox50_TextChanged(object sender, EventArgs e)
		{
		}
		private void label60_Click(object sender, EventArgs e)
		{
		}
		private void textBox49_TextChanged(object sender, EventArgs e)
		{
		}
		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}
		private void button1_Click(object sender, EventArgs e)
		{
			checkSms.Enabled = false;
			checkVms.Enabled = false;
			timer1.Enabled = true;
			button1.Enabled = false;
			label1.Text = "开启";
			//Thread t = new Thread(CheckMatchFileInfo);
			//t.Start();
			//CheckMatchFileInfo();
			
		}

		public void CheckMatchFileInfo() {
			DataFlow d = new DataFlow();
			DataFlowInfo m = new DataFlowInfo();
			 m = d.getAllDataFolwInfo();
			for (int i = 0; i < m.AlldataInfo.Count; i++)
            {
				if (m.AlldataInfo[i].FileStatus == "异常") {
					InsterMysql eventInster = new InsterMysql();
					string eventMsg = m.AlldataInfo[i].FileTime + "时次" + m.AlldataInfo[i].Filename + "资料缺报:" + m.AlldataInfo[i].AbsentSum + "个.";
					eventInster.insterIntoEVENT_LOG(m.AlldataInfo[i].FileTime,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m.AlldataInfo[i].Filename, eventMsg, m.AlldataInfo[i].StationDetail);
					if (m.AlldataInfo[i].IsAlarm == "1") {
						Alarm alm =new Alarm();
						alm.passtext = m.AlldataInfo[i].Filename + "资料缺报:" + m.AlldataInfo[i].AbsentSum + "个.";
						alm.Show();
					}
				}
				if (gjz.Text == m.AlldataInfo[i].Filename)
				{
					gjz_absent.Text = m.AlldataInfo[i].AbsentSum;
					gjz_absent.ForeColor = Color.Red;
					gjz_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gjz_detailInfo.ForeColor = Color.Red;
					gjz_status.ForeColor = Color.Red;
					gjz_time.ForeColor = Color.Red;
					gjz.ForeColor = Color.Red;
					gjz_status.Text = m.AlldataInfo[i].FileStatus;
					gjz_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (qyz.Text == m.AlldataInfo[i].Filename)
				{
					qyz_absent.Text = m.AlldataInfo[i].AbsentSum;
					qyz_absent.ForeColor = Color.Red;
					qyz_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					qyz_detailInfo.ForeColor = Color.Red;
					qyz_status.ForeColor = Color.Red;
					qyz_time.ForeColor = Color.Red;
					qyz.ForeColor = Color.Red;
					qyz_status.Text = m.AlldataInfo[i].FileStatus;
					qyz_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (qyzbufr.Text == m.AlldataInfo[i].Filename)
				{
					qyzbufr_absent.Text = m.AlldataInfo[i].AbsentSum;
					qyzbufr_absent.ForeColor = Color.Red;
					qyzbufr_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					qyzbufr_detailInfo.ForeColor = Color.Red;
					qyzbufr_status.ForeColor = Color.Red;
					qyzbufr_time.ForeColor = Color.Red;
					qyzbufr.ForeColor = Color.Red;
					qyzbufr_status.Text = m.AlldataInfo[i].FileStatus;
					qyzbufr_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (jtz.Text == m.AlldataInfo[i].Filename)
				{
					jtz_absent.Text = m.AlldataInfo[i].AbsentSum;
					jtz_absent.ForeColor = Color.Red;
					jtz_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					jtz_detailInfo.ForeColor = Color.Red;
					jtz_status.ForeColor = Color.Red;
					jtz_time.ForeColor = Color.Red;
					jtz.ForeColor = Color.Red;
					jtz_status.Text = m.AlldataInfo[i].FileStatus;
					jtz_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (js.Text == m.AlldataInfo[i].Filename)
				{
					js_absent.Text = m.AlldataInfo[i].AbsentSum;
					js_absent.ForeColor = Color.Red;
					js_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					js_detailInfo.ForeColor = Color.Red;
					js_status.ForeColor = Color.Red;
					js_time.ForeColor = Color.Red;
					js.ForeColor = Color.Red;
					js_status.Text = m.AlldataInfo[i].FileStatus;
					js_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gk100.Text == m.AlldataInfo[i].Filename)
				{
					gk100_absent.Text = m.AlldataInfo[i].AbsentSum;
					gk100_absent.ForeColor = Color.Red;
					gk100_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gk100_detailInfo.ForeColor = Color.Red;
					gk100_status.ForeColor = Color.Red;
					gk100_time.ForeColor = Color.Red;
					gk100.ForeColor = Color.Red;
					gk100_status.Text = m.AlldataInfo[i].FileStatus;
					gk100_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gk500.Text == m.AlldataInfo[i].Filename)
				{
					gk500_absent.Text = m.AlldataInfo[i].AbsentSum;
					gk500_absent.ForeColor = Color.Red;
					gk500_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gk500_detailInfo.ForeColor = Color.Red;
					gk500_status.ForeColor = Color.Red;
					gk500_time.ForeColor = Color.Red;
					gk500.ForeColor = Color.Red;
					gk500_status.Text = m.AlldataInfo[i].FileStatus;
					gk500_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gkzkhjbcs.Text == m.AlldataInfo[i].Filename)
				{
					gkzkhjbcs_absent.Text = m.AlldataInfo[i].AbsentSum;
					gkzkhjbcs_absent.ForeColor = Color.Red;
					gkzkhjbcs_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gkzkhjbcs_detailInfo.ForeColor = Color.Red;
					gkzkhjbcs_status.ForeColor = Color.Red;
					gkzkhjbcs_time.ForeColor = Color.Red;
					gkzkhjbcs.ForeColor = Color.Red;
					gkzkhjbcs_status.Text = m.AlldataInfo[i].FileStatus;
					gkzkhjbcs_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gkzkhmjzl.Text == m.AlldataInfo[i].Filename)
				{
					gkzkhmjzl_absent.Text = m.AlldataInfo[i].AbsentSum;
					gkzkhmjzl_absent.ForeColor = Color.Red;
					gkzkhmjzl_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gkzkhmjzl_detailInfo.ForeColor = Color.Red;
					gkzkhmjzl_status.ForeColor = Color.Red;
					gkzkhmjzl_time.ForeColor = Color.Red;
					gkzkhmjzl.ForeColor = Color.Red;
					gkzkhmjzl_status.Text = m.AlldataInfo[i].FileStatus;
					gkzkhmjzl_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gkswj.Text == m.AlldataInfo[i].Filename)
				{
					gkswj_absent.Text = m.AlldataInfo[i].AbsentSum;
					gkswj_absent.ForeColor = Color.Red;
					gkswj_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gkswj_detailInfo.ForeColor = Color.Red;
					gkswj_status.ForeColor = Color.Red;
					gkswj_time.ForeColor = Color.Red;
					gkswj.ForeColor = Color.Red;
					gkswj_status.Text = m.AlldataInfo[i].FileStatus;
					gkswj_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (gps.Text == m.AlldataInfo[i].Filename)
				{
					gps_absent.Text = m.AlldataInfo[i].AbsentSum;
					gps_absent.ForeColor = Color.Red;
					gps_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					gps_detailInfo.ForeColor = Color.Red;
					gps_status.ForeColor = Color.Red;
					gps_time.ForeColor = Color.Red;
					gps.ForeColor = Color.Red;
					gps_status.Text = m.AlldataInfo[i].FileStatus;
					gps_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (trsf.Text == m.AlldataInfo[i].Filename)
				{
					trsf_absent.Text = m.AlldataInfo[i].AbsentSum;
					trsf_absent.ForeColor = Color.Red;
					trsf_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					trsf_detailInfo.ForeColor = Color.Red;
					trsf_status.ForeColor = Color.Red;
					trsf_time.ForeColor = Color.Red;
					trsf.ForeColor = Color.Red;
					trsf_status.Text = m.AlldataInfo[i].FileStatus;
					trsf_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (zwx.Text == m.AlldataInfo[i].Filename)
				{
					zwx_absent.Text = m.AlldataInfo[i].AbsentSum;
					zwx_absent.ForeColor = Color.Red;
					zwx_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					zwx_detailInfo.ForeColor = Color.Red;
					zwx_status.ForeColor = Color.Red;
					zwx_time.ForeColor = Color.Red;
					zwx.ForeColor = Color.Red;
					zwx_status.Text = m.AlldataInfo[i].FileStatus;
					zwx_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (qrj.Text == m.AlldataInfo[i].Filename)
				{
					qrj_absent.Text = m.AlldataInfo[i].AbsentSum;
					qrj_absent.ForeColor = Color.Red;
					qrj_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					qrj_detailInfo.ForeColor = Color.Red;
					qrj_status.ForeColor = Color.Red;
					qrj_time.ForeColor = Color.Red;
					qrj.ForeColor = Color.Red;
					qrj_status.Text = m.AlldataInfo[i].FileStatus;
					qrj_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (sybufr.Text == m.AlldataInfo[i].Filename)
				{
					sybufr_absent.Text = m.AlldataInfo[i].AbsentSum;
					sybufr_absent.ForeColor = Color.Red;
					sybufr_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					sybufr_detailInfo.ForeColor = Color.Red;
					sybufr_status.ForeColor = Color.Red;
					sybufr_time.ForeColor = Color.Red;
					sybufr.ForeColor = Color.Red;
					sybufr_status.Text = m.AlldataInfo[i].FileStatus;
					sybufr_time.Text = m.AlldataInfo[i].FileTime;
				}
				if (jxh.Text == m.AlldataInfo[i].Filename)
				{
					jxh_absent.Text = m.AlldataInfo[i].AbsentSum;
					jxh_absent.ForeColor = Color.Red;
					jxh_detailInfo.Text = m.AlldataInfo[i].StationDetail;
					jxh_detailInfo.ForeColor = Color.Red;
					jxh_status.ForeColor = Color.Red;
					jxh_time.ForeColor = Color.Red;
					jxh.ForeColor = Color.Red;
					jxh_status.Text = m.AlldataInfo[i].FileStatus;
					jxh_time.Text = m.AlldataInfo[i].FileTime;
				}
			}
		}
		private void button10_Click(object sender, EventArgs e)
		{
			getSubsystem_ALLConfig d = new getSubsystem_ALLConfig();
			SubConfig m = d.getStationInfo();
			for (int i = 0; i < m.subsystem_config.Count; i++)
			{
				if (m.subsystem_config[i].data_name == (ZKqyz_name.Text))
				{
					if (ZKqyz.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKqyz_sum.Text;
					m.subsystem_config[i].absent_sum = ZKqyz_absent.Text;
					m.subsystem_config[i].monitor_time = ZKqyz_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgjzbufr_name.Text))
				{
					if (ZKgjzbufr.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgjzbufr_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgjzbufr_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgjzbufr_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKqyzbufr_name.Text))
				{
					if (ZKqyzbufr.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKqyzbufr_sum.Text;
					m.subsystem_config[i].absent_sum = ZKqyzbufr_absent.Text;
					m.subsystem_config[i].monitor_time = ZKqyzbufr_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKjtz_name.Text))
				{
					if (ZKjtz.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKjtz_sum.Text;
					m.subsystem_config[i].absent_sum = ZKjtz_absent.Text;
					m.subsystem_config[i].monitor_time = ZKjtz_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKjs_name.Text))
				{
					if (ZKjs.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKjs_sum.Text;
					m.subsystem_config[i].absent_sum = ZKjs_absent.Text;
					m.subsystem_config[i].monitor_time = ZKjs_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgk100Pa_name.Text))
				{
					if (ZKgk100Pa.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgk100Pa_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgk100Pa_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgk100Pa_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgk500Pa_name.Text))
				{
					if (ZKgk500Pa.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgk500Pa_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgk500Pa_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgk500Pa_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgkzkhjbcs_name.Text))
				{
					if (ZKgkzkhjbcs.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgkzkhjbcs_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgkzkhjbcs_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgkzkhjbcs_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgkzkhmjzl_name.Text))
				{
					if (ZKgkzkhmjzl.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgkzkhmjzl_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgkzkhmjzl_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgkzkhmjzl_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgkswj_name.Text))
				{
					if (ZKgkswj.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgkswj_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgkswj_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgkswj_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKtrsf_name.Text))
				{
					if (ZKtrsf.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKtrsf_sum.Text;
					m.subsystem_config[i].absent_sum = ZKtrsf_absent.Text;
					m.subsystem_config[i].monitor_time = ZKtrsf_time.Text;
				}
				if (m.subsystem_config[i].data_name == (ZKgps_name.Text))
				{
					if (ZKgps.Checked == true)
					{
						m.subsystem_config[i].nend_check = "1";
					}
					else
					{
						m.subsystem_config[i].nend_check = "0";
					}
					m.subsystem_config[i].sum = ZKgps_sum.Text;
					m.subsystem_config[i].absent_sum = ZKgps_absent.Text;
					m.subsystem_config[i].monitor_time = ZKgps_time.Text;
				}

			}
			if (MessageBox.Show("确定要同步吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
			{
				UpdateSubsystem_ALLConfig dd = new UpdateSubsystem_ALLConfig();
				if (dd.UpDatemysql(m)) {
					MessageBox.Show("同步成功!");
					button7.PerformClick();
				}
			}
		}
		private void button7_Click(object sender, EventArgs e)
		{
			getSubsystem_ALLConfig d = new getSubsystem_ALLConfig();
			SubConfig m = d.getStationInfo();
			for (int i = 0; i < m.subsystem_config.Count; i++)
			{
				if (m.subsystem_config[i].data_name == (ZKgjzbufr_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgjzbufr.Checked = true;
					}
					ZKgjzbufr_sum.Text = m.subsystem_config[i].sum;
					ZKgjzbufr_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgjzbufr_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKqyzbufr_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKqyzbufr.Checked = true;
					}
					ZKqyzbufr_sum.Text = m.subsystem_config[i].sum;
					ZKqyzbufr_absent.Text = m.subsystem_config[i].absent_sum;
					ZKqyzbufr_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKqyz_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKqyz.Checked = true;
					}
					ZKqyz_sum.Text = m.subsystem_config[i].sum;
					ZKqyz_absent.Text = m.subsystem_config[i].absent_sum;
					ZKqyz_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKjtz_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKjtz.Checked = true;
					}
					ZKjtz_sum.Text = m.subsystem_config[i].sum;
					ZKjtz_absent.Text = m.subsystem_config[i].absent_sum;
					ZKjtz_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKjs_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKjs.Checked = true;
					}
					ZKjs_sum.Text = m.subsystem_config[i].sum;
					ZKjs_absent.Text = m.subsystem_config[i].absent_sum;
					ZKjs_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgk100Pa_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgk100Pa.Checked = true;
					}
					ZKgk100Pa_sum.Text = m.subsystem_config[i].sum;
					ZKgk100Pa_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgk100Pa_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgk500Pa_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgk500Pa.Checked = true;
					}
					ZKgk500Pa_sum.Text = m.subsystem_config[i].sum;
					ZKgk500Pa_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgk500Pa_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgkzkhjbcs_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgkzkhjbcs.Checked = true;
					}
					ZKgkzkhjbcs_sum.Text = m.subsystem_config[i].sum;
					ZKgkzkhjbcs_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgkzkhjbcs_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgkzkhmjzl_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgkzkhmjzl.Checked = true;
					}
					ZKgkzkhmjzl_sum.Text = m.subsystem_config[i].sum;
					ZKgkzkhmjzl_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgkzkhmjzl_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgkswj_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgkswj.Checked = true;
					}
					ZKgkswj_sum.Text = m.subsystem_config[i].sum;
					ZKgkswj_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgkswj_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKtrsf_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKtrsf.Checked = true;
					}
					ZKtrsf_sum.Text = m.subsystem_config[i].sum;
					ZKtrsf_absent.Text = m.subsystem_config[i].absent_sum;
					ZKtrsf_time.Text = m.subsystem_config[i].monitor_time;
				}
				if (m.subsystem_config[i].data_name == (ZKgps_name.Text))
				{
					if (m.subsystem_config[i].nend_check == "1")
					{
						ZKgps.Checked = true;
					}
					ZKgps_sum.Text = m.subsystem_config[i].sum;
					ZKgps_absent.Text = m.subsystem_config[i].absent_sum;
					ZKgps_time.Text = m.subsystem_config[i].monitor_time;
				}
			}
			button10.Enabled = true;
		}
		private void groupBox4_Enter(object sender, EventArgs e)
		{
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			button10.Enabled = false;
			UpdateSmsSendResult();
		}
		public void UpdateSmsSendResult()
		{

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
				String sqlSelect = @"SELECT * FROM `SMS_LOG` order by eventID DESC limit 0,50";
				// 创建用于实现MySQL语句的对象
				MySqlCommand mySqlCommand2 = new MySqlCommand(sqlSelect, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
																							// 执行MySQL语句，接收查询到的MySQL结果
				MySqlDataReader mdr = mySqlCommand2.ExecuteReader();
				// 读取数据
				int i = 0;
				while (mdr.Read())
				{
					int index = this.dataGridView3.Rows.Add();
					this.dataGridView3.Rows[index].Cells[0].Value = mdr.GetString("datetime");
					this.dataGridView3.Rows[index].Cells[1].Value = mdr.GetString("enentMsg");
					this.dataGridView3.Rows[index].Cells[2].Value = mdr.GetString("mobile");
					this.dataGridView3.Rows[index].Cells[3].Value = mdr.GetString("sensResult");
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
			private void label68_Click(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			label1.Text = "关闭";
			timer1.Enabled = false;
			button2.Enabled = false;
			button1.Enabled = true;
			checkSms.Enabled = true;
			checkVms.Enabled = true;
			//mainThread d = new mainThread();
			//d.test();

		}

        private void 总控详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Form2 d = new Form2();
			d.Show();
        }

        private void 短信助手ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			login d = new login();
			d.Show();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
		public void SendSmstoStation() {
			mainThread smssend = new mainThread();
			smssend.test();
		}
        private void timer1_Tick(object sender, EventArgs e)
        {
			if (checkSms.Checked) {
				Thread SendThread = new Thread(SendSmstoStation);
				SendThread.Start();
			}
			if (checkVms.Checked) { 
			
			}
			CheckMatchFileInfo();
		}

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> f73748138f825c1c55d370238f6f87af227f6b6f
