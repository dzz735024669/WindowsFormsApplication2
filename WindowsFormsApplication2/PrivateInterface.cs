using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class PrivateDataItem
    {

        public string isRadar { get; set; }

        public string ctsInTimeState { get; set; }

        public string province { get; set; }

        public string processStartTime { get; set; }

        public string municipality { get; set; }

        public string county { get; set; }

        public string stationName { get; set; }

        public string cTeldataProcesser { get; set; }

        public string stationID { get; set; }

        public string cTelManager { get; set; }
    }

    public class PrivateDetailInfo
    {

        public List<PrivateDataItem> data { get; set; }

        public string message { get; set; }

        public string statusCode { get; set; }
    }
    public class PrivateBasicData
    {
        /// <summary>
        /// 
        /// </summary>
        public string ctsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double checkCtsInTimeRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string checkRealityAccept { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double checkCtsRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string realityAccpet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oughtAccept { get; set; }

        /// <summary>
        /// 质控后地面自动站气象要素资料(区域站)_一体化
        /// </summary>
        public string dataName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string countTime { get; set; }

        /// <summary>
        /// 中国地面逐小时资料-要素存储
        /// </summary>
        public string sodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double ctsInTimeRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string checkOughtAccept { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double ctsRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dpc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }

    }

    public class PrivateBasicInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public PrivateBasicData data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string statusCode { get; set; }

    }
    class PrivateInterface
    {
        /// <summary>
        /// 获取cookies，并返回。
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            var client = new RestClient("http://10.129.89.183/tenant/api/v1/user/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "38");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "10.129.89.183");
            request.AddHeader("Postman-Token", "71697baf-774c-402b-b9ca-6bcf58cf2310,9df49532-c863-4f3d-98cb-87ad7eab4c70");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"passwd\":\"*\",\"email\":\"*\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //var contentPost = response.Content;
            var json = response.Content;
            try
            {
                //string json;
                //json = Type;
                int i = json.IndexOf("token");//找a的位置  
                int j = json.IndexOf("\"},\"mode");//找b的位置  
                json = (json.Substring(i + 8)).Substring(0, j - i - 8);//找出a和b之间的字符串
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "参数错误";
            }
        }
        /// <summary>
        /// 获取国家集基础信息接口-私有接口
        /// </summary>
        /// <param name="time">2021-06-04 12:00</param>
        /// <param name="ctscode">四级编码</param>
        /// <param name="dpccode">四级编码</param>
        /// <param name="sodcode">四级编码</param>
        /// <returns></returns>
        public PrivateBasicInfo PrivateBasicInterfaceGuoJiaJi(string datetime, string ctscode, string dpccode, string sodcode)
        {

            DateTime dt = DateTime.UtcNow;
            //string datetime = dt.ToString("yyyy-MM-dd HH:00");
            var client = new RestClient("http://10.129.89.183/dataflow/service/api/v1/report/station/allBasicInfo");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string token = GetToken();
            request.AddCookie("token", token);
            request.AddHeader("Cookie", "token=" + token + "; __guid=91317274.1702450738849864700.1573116449700.0889; language=zh_CN; monitor_count=11; skin=white");
            request.AddHeader("tenantId", "e10adc3949ba59abbe56e057f20f88dd");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=26589b02adac3713513269dbca2501179dfa0a0c3253ee3490836f70b9c03541");
            request.AddParameter("application/json", "{\r\n    \"apikey\": \"e10adc3949ba59abbe56e057f2gg88dd\",\r\n    \"dataName\": \"CTS:质控后地面自动站降水量资料_一体化\\nSOD:中国地面分钟降水资料\",\r\n    \"ctsInTime\": \"4\",\r\n    \"dataType\": \"全部\",\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"" + datetime + "\",\r\n    \"classify\": \"考核应收\",\r\n    \"cts\": \"" + ctscode + "\",\r\n    \"dpc\": \"" + dpccode + "\",\r\n    \"sod\": \"" + sodcode + "\",\r\n    \"flag\": 0\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string s = response.Content;
            //Console.WriteLine(response.Content);
            PrivateBasicInfo res = JsonConvert.DeserializeObject<PrivateBasicInfo>(s);
            PrivateBasicData ddd = res.data;
           // string checkrealsum = ddd.checkRealityAccept;
            return res;
        }
        /// <summary>
        /// 获取本省收集基础信息接口-私有接口
        /// </summary>
        /// <param name="time">2021-06-04 12:00</param>
        /// <param name="ctscode">四级编码</param>
        /// <param name="dpccode">四级编码</param>
        /// <param name="sodcode">四级编码</param>
        /// <returns></returns>
        public PrivateBasicInfo PrivateBasicInterfaceBensheng(string datetime,string ctscode, string dpccode, string sodcode)
        {

            DateTime dt = DateTime.UtcNow;
            //string datetime = dt.ToString("yyyy-MM-dd HH:00");
            var client = new RestClient("http://10.129.89.183/dataflow/service/api/v1/report/station/basicInfo");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string token = GetToken();
            request.AddCookie("token", token);
            request.AddHeader("Cookie", "token=" + token + "; __guid=91317274.1702450738849864700.1573116449700.0889; language=zh_CN; monitor_count=11; skin=white");
            request.AddHeader("tenantId", "e10adc3949ba59abbe56e057f20f88dd");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=26589b02adac3713513269dbca2501179dfa0a0c3253ee3490836f70b9c03541");
            request.AddParameter("application/json", "{\r\n    \"apikey\": \"e10adc3949ba59abbe56e057f2gg88dd\",\r\n    \"dataName\": \"CTS:质控后地面自动站降水量资料_一体化\\nSOD:中国地面分钟降水资料\",\r\n    \"ctsInTime\": \"4\",\r\n    \"dataType\": \"全部\",\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"" + datetime + "\",\r\n    \"classify\": \"考核应收\",\r\n    \"cts\": \"" + ctscode + "\",\r\n    \"dpc\": \"" + dpccode + "\",\r\n    \"sod\": \"" + sodcode + "\",\r\n    \"flag\": 0\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string s = response.Content;
            //Console.WriteLine(response.Content);
            PrivateBasicInfo res = JsonConvert.DeserializeObject<PrivateBasicInfo>(s);
            PrivateBasicData ddd = res.data;
            //string checkrealsum = ddd.checkRealityAccept;
            return res;
        }

        public PrivateDetailInfo PrivateDetailInterfaceBensheng(string time, string ctscode, string dpccode, string sodcode, string ctsInTime) {

            var client = new RestClient("http://10.129.89.181/dataflow/service/api/v1/report/station/detailInfo");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string token = GetToken();
            request.AddCookie("token", token);
            request.AddHeader("tenantId", "e10adc3949ba59abbe56e057f20f88dd");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=6f930b529be4a2ee8f08d9254fa6f6ab2b5cf6834299c262757223fc8331c46a");
            request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"" + time + "\",\r\n    \"cts\": \"" + ctscode + "\",\r\n    \"dpc\": \"" + dpccode + "\",\r\n    \"sod\": \"" + sodcode + "\",\r\n    \"ctsInTime\": \"" + ctsInTime + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string s = response.Content;
            PrivateDetailInfo res = JsonConvert.DeserializeObject<PrivateDetailInfo>(s);
            return res;
        }

        public PrivateDetailInfo PrivateDetailInterfaceGuoJiaJi(string time, string ctscode, string dpccode, string sodcode, string ctsInTime)
        {

            var client = new RestClient("http://10.129.89.181/dataflow/service/api/v1/report/station/allDetailInfo");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string token = GetToken();
            request.AddCookie("token", token);
            request.AddHeader("tenantId", "e10adc3949ba59abbe56e057f20f88dd");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=6f930b529be4a2ee8f08d9254fa6f6ab2b5cf6834299c262757223fc8331c46a");
            request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"" + time + "\",\r\n    \"cts\": \"" + ctscode + "\",\r\n    \"dpc\": \"" + dpccode + "\",\r\n    \"sod\": \"" + sodcode + "\",\r\n    \"ctsInTime\": \"" + ctsInTime + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string s = response.Content;
            PrivateDetailInfo res = JsonConvert.DeserializeObject<PrivateDetailInfo>(s);
            return res;
        }

    }
}

