using RestSharp;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WindowsFormsApplication2
{
    public class PublicBasicData
    {

        public string ctsCode { get; set; }

        public string checkRealityAccept { get; set; }

        public string checkCtsInTimeRate { get; set; }

        public string ctsInTimeRate { get; set; }

        public string checkCtsRate { get; set; }

        public string checkOughtAccept { get; set; }

        public string ctsRate { get; set; }

        public string realityAccpet { get; set; }

        public string ctsName { get; set; }

        public string oughtAccept { get; set; }

        public string time { get; set; }
    }
    public class PublicBasicInfo
    {

        public PublicBasicData data { get; set; }

        public string message { get; set; }

        public string statusCode { get; set; }
    }
    public class DetailDataItem
    {

        public string isRadar { get; set; }

        public string ctsInTimeState { get; set; }

        public string province { get; set; }

        public string municipality { get; set; }

        public string county { get; set; }

        public string stationName { get; set; }

        public string cTeldataProcesser { get; set; }

        public string stationID { get; set; }

        public string cTelManager { get; set; }
    }

    public class PublicDetailInfo
    {
        public List<DetailDataItem> data { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
    }
    class PublicInterface
    {        
        /// <summary>
        /// 获取国家集基础信息接口
        /// </summary>
        /// <param name="time">2021-06-04 12:00</param>
        /// <param name="ctscode">四级编码</param>
        /// <param name="dpccode">四级编码</param>
        /// <param name="sodcode">四级编码</param>
        /// <returns></returns>
        public PublicBasicInfo PublicBasicInterface(string time, string ctscode, string dpccode, string sodcode) {
            var client = new RestClient("http://10.129.89.183/dataflow/service/openapi/v1/country/basicInfo?apikey=e10adc3949ba59abbe56e057f2gg88dd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \""+time+"\",\r\n    \"cts\": \""+ ctscode + "\",\r\n    \"dpc\": \""+dpccode+"\",\r\n    \"sod\": \""+sodcode+"\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"2021-06-07 08:00\",\r\n    \"cts\": \"A.0001.0044.R001\",\r\n    \"dpc\": \"A.0011.0001.P002\",\r\n    \"sod\": \"A.0011.0001.S001\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string result = response.Content;
            PublicBasicInfo rb = JsonConvert.DeserializeObject<PublicBasicInfo>(result);
            return rb;
        }
        /// <summary>
        /// 获取国家集详情接口
        /// </summary>
        /// <param name="time">2021-06-04 12:00</param>
        /// <param name="ctscode">四级编码</param>
        /// <param name="dpccode">四级编码</param>
        /// <param name="sodcode">四级编码</param>
        /// <param name="ctsInTime">0：全部(应收)，1：及时，2：逾限，3：过期，4：缺收，5：实收 </param>
        /// <returns></returns>
        public PublicDetailInfo PublicDetailInterface(string time, string ctscode, string dpccode, string sodcode, string ctsInTime)
        {
            var client = new RestClient("http://10.129.89.183/dataflow/service/openapi/v1/country/detailInfo?apikey=e10adc3949ba59abbe56e057f2gg88dd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"" + time + "\",\r\n    \"cts\": \"" + ctscode + "\",\r\n    \"dpc\": \"" + dpccode + "\",\r\n    \"sod\": \"" + sodcode + "\",\r\n    \"ctsInTime\": \"" + ctsInTime + "\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"province\": \"安徽省\",\r\n    \"time\": \"2021-06-04 13:00\",\r\n    \"cts\": \"A.0001.0044.R001\",\r\n    \"dpc\": \"A.0011.0001.P002\",\r\n    \"sod\": \"A.0011.0001.S001\",\r\n    \"ctsInTime\": \"5\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string result = response.Content;
            PublicDetailInfo rb = JsonConvert.DeserializeObject<PublicDetailInfo>(result);
            return rb;
        }
    }
}
