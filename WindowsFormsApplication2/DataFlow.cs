using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static WindowsFormsApplication2.getIrregularSubsystem_config;
using static WindowsFormsApplication2.getStationInfo;
using static WindowsFormsApplication2.getSubsystem_ALLConfig;

namespace WindowsFormsApplication2
{
    class DataFlow
    {
        public class DataFlowItem
        {

            public string Filename { get; set; }

            public string FileTime { get; set; }

            public string AbsentSum { get; set; }

            public string FileStatus { get; set; }

            public string StationDetail { get; set; }

            public string IsAlarm { get; set; }

        }
       
       
        public class DataFlowInfo
        {
            public List<DataFlowItem> AlldataInfo { get; set; }
        }
        static List<DataFlowItem> listInfo = new List<DataFlowItem>();
        static DataFlowInfo allInfo = new DataFlowInfo();
        static DataFlowItem itemInfo = new DataFlowItem();
        //main
        public DataFlowInfo getAllDataFolwInfo()
        {
            listInfo.Clear();
            allInfo.AlldataInfo = listInfo;

            return foreachsubconfig(getConfig(), getIrregulartConfig());
           // return allInfo;
        }
        public SubConfig getConfig()
        {
            getSubsystem_ALLConfig subconfig = new getSubsystem_ALLConfig();
            return subconfig.getStationInfo();
        }

        //获取不规则数据库

        public irregularAllConfig getIrregulartConfig()
        {
            getIrregularSubsystem_config IrregularSubconfig = new getIrregularSubsystem_config();
            return IrregularSubconfig.irregularDataFlowInfo();
        }
        //遍历数据库
        public DataFlowInfo foreachsubconfig(SubConfig subconfig, irregularAllConfig IrregularSubconfig)
        {
           // DataFlowInfo allInfo = new DataFlowInfo();
            foreach (Subsystem_ALLConfig m in subconfig.subsystem_config)
            {
                string dt = DateTime.UtcNow.ToString("yyyy-MM-dd HH" + ":00");
                getDataFlowInfo mm = new getDataFlowInfo();
       
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
                mm.getItemInfo();
                


            }
            foreach (irregularSubsystem_ALLConfig m in IrregularSubconfig.date)
            {
                string dt = DateTime.UtcNow.ToString("yyyy-MM-dd HH" + ":00");
                getIrregularDataFlowInfo Irrmm = new getIrregularDataFlowInfo();

                Irrmm.dateTime = dt;
                Irrmm.config_id = m.config_id;
                Irrmm.cts_code = m.cts_code;
                Irrmm.sod_code = m.sod_code;
                Irrmm.dpc_code = m.dpc_code;
                Irrmm.data_name = m.data_name;
                Irrmm.data_type = m.data_type;
                Irrmm.nend_check = m.nend_check;
                Irrmm.business_frequency = m.business_frequency;
                Irrmm.monitor_time = m.monitor_time;
                Irrmm.sum = m.sum;
                Irrmm.absent_sum = m.absent_sum;
                Irrmm.all_station_info = m.all_station_info;
                Irrmm.sms_send = m.sms_send;
                Irrmm.vms_send = m.vms_send;
                Irrmm.send_absentSum = m.send_absentSum;
                Irrmm.HourAdd = m.HourAdd;
                Irrmm.MinAdd = m.MinAdd;
                Irrmm.getIrrItemInfo();



            }


            return sleepThread();

        }
        public DataFlowInfo sleepThread() {
           
            allInfo.AlldataInfo = listInfo;
            return allInfo;
        }
        class getDataFlowInfo
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
            public void getItemInfo()
            {
                DataFlowItem itemInfo = new DataFlowItem();
                // int NOSendSum = Convert.ToInt32(send_absentSum);
                DateTime dt = DateTime.UtcNow;
                if (nend_check == "1")
                {
                    if (business_frequency.Contains(dt.ToString("HH")))
                    {
                        //if(true)
                        if (monitor_time == dt.ToString("mm"))
                        {
                            //启动国家局公共接口查询
                            try {
                                PublicInterface PubInfo = new PublicInterface();
                                PublicDetailInfo pubDetailInfo = PubInfo.PublicDetailInterface(dateTime, cts_code, dpc_code, sod_code, "5");
                                PublicBasicInfo pubBaseInfo = PubInfo.PublicBasicInterface(dateTime, cts_code, dpc_code, sod_code);
                                string[] PubDetailStation = new string[pubDetailInfo.data.Count];
                                for (int i = 0; i < pubDetailInfo.data.Count; i++)
                                {
                                    PubDetailStation[i] = pubDetailInfo.data[i].stationID;
                                }
                            
                            //国家局缺报站点
                            string[] PubAbsentStation = all_station_info.Split(',').Except(PubDetailStation).ToArray();
                            //启动本省接口查询
                            PrivateInterface PriInfo = new PrivateInterface();
                            PrivateDetailInfo PriDeatilInfo = PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                                if (PriDeatilInfo==null) {
                                    PriDeatilInfo = PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                                    if (PriDeatilInfo == null)
                                    {
                                        PriDeatilInfo = PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                                        if (PriDeatilInfo == null)
                                        {
                                            PriDeatilInfo = PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                                        }
                                    }
                                }
                            string[] PriDetailStation = new string[PriDeatilInfo.data.Count];
                            for (int i = 0; i < PriDeatilInfo.data.Count; i++)
                            {
                                PriDetailStation[i] = PriDeatilInfo.data[i].stationID;
                            }
                            string[] PriAbsentStation = all_station_info.Split(',').Except(PriDetailStation).ToArray();
                            string[] PubAndPriAbsentStation = PubAbsentStation.Intersect(PriAbsentStation).ToArray();
                                itemInfo.AbsentSum = (Convert.ToInt32(sum) - Convert.ToInt32(pubBaseInfo.data.realityAccpet)).ToString();
                                //itemInfo.AbsentSum = PubAndPriAbsentStation.Count().ToString();
                            itemInfo.Filename = data_name;
                            if (Convert.ToInt32(itemInfo.AbsentSum) > Convert.ToInt32(absent_sum)) {
                                itemInfo.FileStatus = "异常";
                                itemInfo.IsAlarm = "1";
                            } else {
                                itemInfo.FileStatus = "正常";
                                itemInfo.IsAlarm = "0";
                            }
                            itemInfo.FileTime = dateTime;
                            for (int i = 0; i < PubAndPriAbsentStation.Count(); i++) {
                                itemInfo.StationDetail = PubAndPriAbsentStation[i] + "," + itemInfo.StationDetail;
                            }

                            listInfo.Add(itemInfo);
                        } catch { }

                        }
                        else
                        {
                                //不发送短信通知
                        }
                        
                    }
                }
                //int sum = getSationSum(dateTime, cts, dpc, sod);
            }
        }
        class getIrregularDataFlowInfo
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
            public string MinAdd;
            public string HourAdd;
            public void getIrrItemInfo()
            {
                DataFlowItem itemInfo = new DataFlowItem();
                // int NOSendSum = Convert.ToInt32(send_absentSum);
                DateTime dt = DateTime.UtcNow;
                if (nend_check == "1")
                {
                    if (business_frequency.Contains(dt.ToString("HH")))
                    {
                        //if (true)
                        if (monitor_time == dt.ToString("mm"))
                        {
                            try { 
                            //启动国家局公共接口查询
                            PublicInterface PubInfo = new PublicInterface();

                            string fileTime = dt.AddHours(Convert.ToInt32(HourAdd)).AddMinutes(Convert.ToInt32(MinAdd)).ToString("yyyy-MM-dd HH:mm");

                            PublicDetailInfo pubDetailInfo = PubInfo.PublicDetailInterface(fileTime, cts_code, dpc_code, sod_code, "5");
                            PublicBasicInfo pubBaseInfo = PubInfo.PublicBasicInterface(fileTime, cts_code, dpc_code, sod_code);
                                string[] PubDetailStation = new string[pubDetailInfo.data.Count];
                            for (int i = 0; i < pubDetailInfo.data.Count; i++)
                            {
                                PubDetailStation[i] = pubDetailInfo.data[i].stationID;
                            }
                            //国家局缺报站点
                            string[] PubAbsentStation = all_station_info.Split(',').Except(PubDetailStation).ToArray();
                            //启动本省接口查询
                            PrivateInterface PriInfo = new PrivateInterface();
                            PrivateDetailInfo PriDeatilInfo = PriInfo.PrivateDetailInterfaceBensheng(dateTime, cts_code, dpc_code, sod_code, "5");
                            string[] PriDetailStation = new string[PriDeatilInfo.data.Count];
                            for (int i = 0; i < PriDeatilInfo.data.Count; i++)
                            {
                                PriDetailStation[i] = PriDeatilInfo.data[i].stationID;
                            }
                            string[] PriAbsentStation = all_station_info.Split(',').Except(PriDetailStation).ToArray();
                            string[] PubAndPriAbsentStation = PubAbsentStation.Intersect(PriAbsentStation).ToArray();
                             itemInfo.AbsentSum = (Convert.ToInt32(sum) - Convert.ToInt32(pubBaseInfo.data.realityAccpet)).ToString(); 
                            //itemInfo.AbsentSum = PubAndPriAbsentStation.Count().ToString();
                            itemInfo.Filename = data_name;
                            if (Convert.ToInt32(itemInfo.AbsentSum) > Convert.ToInt32(absent_sum))
                            {
                                itemInfo.FileStatus = "异常";
                                itemInfo.IsAlarm = "1";
                            }
                            else
                            {
                                itemInfo.FileStatus = "正常";
                                itemInfo.IsAlarm = "0";
                            }
                            itemInfo.FileTime = fileTime;
                            for (int i = 0; i < PubAndPriAbsentStation.Count(); i++)
                            {
                                itemInfo.StationDetail = PubAndPriAbsentStation[i] + "," + itemInfo.StationDetail;
                            }
                            }
                            catch { }
                            listInfo.Add(itemInfo);
                           

                        }
                        else
                        {
                            //不发送短信通知
                        }

                    }
                }
                //int sum = getSationSum(dateTime, cts, dpc, sod);
            }
        }
    }
}
