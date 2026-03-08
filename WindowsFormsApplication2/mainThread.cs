using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static WindowsFormsApplication2.getStationInfo;
using static WindowsFormsApplication2.getSubsystem_ALLConfig;

namespace WindowsFormsApplication2
{
    class mainThread
    {
        //获取配置文件
        public SubConfig getConfig() {
            getSubsystem_ALLConfig subconfig = new getSubsystem_ALLConfig();
            return subconfig.getStationInfo();
        }
        public void test() {
            foreachsubconfig(getConfig());
        }
        //遍历数据库
        public void foreachsubconfig(SubConfig subconfig) {
            foreach (Subsystem_ALLConfig m in subconfig.subsystem_config) {
                string dt = DateTime.UtcNow.ToString("yyyy-MM-dd HH"+":00");
                sengSms mm = new sengSms();
                mm.dateTime = dt;                
                mm.config_id = m.config_id;
                mm.cts_code = m.cts_code;
                mm.sod_code = m.sod_code;
                mm.dpc_code = m.dpc_code;
                mm.data_name = m.data_name;
                mm.data_type = m.data_type;
                mm.nend_check = m.nend_check;
                mm.business_frequency = m.business_frequency;
                mm.monitor_time = m.monitor_time;
                mm.sum = m.sum;
                mm.absent_sum = m.absent_sum;
                mm.all_station_info = m.all_station_info;
                mm.sms_send = m.sms_send;
                mm.vms_send = m.vms_send;
                mm.send_absentSum = m.send_absentSum;
                Thread t = new Thread(new ThreadStart(mm.Sms));
                t.Start();

               
            }
        }
        //获取到报站点总数
        public int getSationSum(string dateTime,string cts,string sod,string dpc) {
            PublicInterface pubGuojiazhan = new PublicInterface();
            string sum=pubGuojiazhan.PublicBasicInterface(dateTime, cts,dpc,sod).data.realityAccpet;
            int m =Convert.ToInt32(sum);
            return m;
        }

        public PublicDetailInfo getDetailInfo(string dateTime, string cts, string sod, string dpc) {
            PublicInterface pubGuojiazhan = new PublicInterface();
            PublicDetailInfo detaliInfo= pubGuojiazhan.PublicDetailInterface(dateTime, cts, dpc, sod, "5");
            return detaliInfo;

        }
        //开启多线程进行语言外呼

        //开启多线程进行短信发送

        //线程等待语言外呼结果



    }
    class sengSms
    {

        public string dateTime;
        public string config_id;
        public string cts_code;
        public string sod_code;
        public string dpc_code;
        public string data_name;
        public string data_type;
        public string nend_check;
        public string business_frequency;
        public string monitor_time;
        public string sum;
        public string absent_sum;
        public string all_station_info;
        public string sms_send;
        public string vms_send;
        public string send_absentSum;
        public void Sms()
        {
            int NOSendSum=Convert.ToInt32(send_absentSum);
            DateTime dt = DateTime.UtcNow;
            if (sms_send=="1") { 
                if (business_frequency.Contains(dt.ToString("HH"))) {
                    if (monitor_time == dt.ToString("mm")) {
                        //启动国家局公共接口查询
                        PublicInterface PubInfo = new PublicInterface();
                        PublicDetailInfo pubDetailInfo=PubInfo.PublicDetailInterface(dateTime, cts_code, dpc_code, sod_code, "5");
                        string[] PubDetailStation = new string[pubDetailInfo.data.Count];
                        for (int i=0;i< pubDetailInfo.data.Count;i++) {
                            PubDetailStation[i] = pubDetailInfo.data[i].stationID;
                        }
                        string[] PubAbsentStation = all_station_info.Split(',').Except(PubDetailStation).ToArray();
                        if (PubAbsentStation.Count() <= NOSendSum)
                        {   //启动本省接口查询
                            PrivateInterface PriInfo = new PrivateInterface();
                            PrivateDetailInfo PriDeatilInfo= PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                            string[] PriDetailStation = new string[PriDeatilInfo.data.Count];
                            for (int i = 0; i < PriDeatilInfo.data.Count; i++)
                            {
                                PriDetailStation[i] = PriDeatilInfo.data[i].stationID;
                            }
                            string[] PriAbsentStation = all_station_info.Split(',').Except(PriDetailStation).ToArray();
                            string[] PubAndPriAbsentStation = PubAbsentStation.Intersect(PriAbsentStation).ToArray();
                            for(int i = 0; i < PubAndPriAbsentStation.Count(); i++) {
                                getStationInfo getPhone = new getStationInfo();
                                StationInfo allPhome= getPhone.getStationInfoByStationID(PubAndPriAbsentStation[i], data_type);
                                SMS sendSmsbyStation = new SMS();
                                string smsmsg = "温馨提示:"+allPhome.StationName+"气象局,国际时:"+dt.ToString("HH")+"时,"+data_name+"数据缺发,收到后请补传.如有疑问联系0551-62290268.";
                                sendSmsbyStation.SendSms(allPhome.DutyPhone, smsmsg);
                                if (allPhome.DutyPhone2!=null) { sendSmsbyStation.SendSms(allPhome.DutyPhone2, smsmsg); }
                                if (allPhome.DutyPhone3!=null) { sendSmsbyStation.SendSms(allPhome.DutyPhone3, smsmsg); }
                                
                            }

                        }
                        else {
                            //不发送短信通知
                        }
                    }
                }
            }
            //int sum = getSationSum(dateTime, cts, dpc, sod);
        }
    }
}
