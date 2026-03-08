using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{

    class cmadaasPublicInterface
    {
        public class PublicSTATION_INFOSItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string STATION_NUM { get; set; }
            /// <summary>
            /// 郎溪
            /// </summary>
            public string STATION_NAME { get; set; }
            /// <summary>
            /// 安徽省
            /// </summary>
            public string STATION_PROVINCE { get; set; }
            /// <summary>
            /// 宣城市
            /// </summary>
            public string STATION_CITY { get; set; }
            /// <summary>
            /// 中国
            /// </summary>
            public string STATION_COUNTRY { get; set; }
            /// <summary>
            /// 郎溪县
            /// </summary>
            public string STATION_DISTRICT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string STATION_STATUS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PROCESS_END_TIME { get; set; }
        }

        public class cmadaasPublicDetailInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public List<PublicSTATION_INFOSItem> STATION_INFOS { get; set; }
        }
        public cmadaasPublicDetailInfo cmadaasPublicDetailInterface(string time, string ctscode,  string sodcode) {

            var client = new RestClient("http://10.129.89.181/cmadaas-dataflow/mcp/queryStationInfos?apikey=e10adc3949ba59abbe56e057f20f88dd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=6f930b529be4a2ee8f08d9254fa6f6ab2b5cf6834299c262757223fc8331c46a");
            request.AddParameter("application/json", "{\r\n    \"co_state\": \"ACTUAL\",\r\n    \"time\": \""+ time + "\",\r\n    \"cts\": \""+ ctscode + "\",\r\n    \"sod\": \""+ sodcode + "\",\r\n    \"area\": \"340000\",\r\n    \"province\": \"\",\r\n    \"city\": \"\",\r\n    \"district\": null,\r\n    \"ischeck\": \"0\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string result = response.Content;
            cmadaasPublicDetailInfo rb = JsonConvert.DeserializeObject<cmadaasPublicDetailInfo>(result);
            return rb;
        }
    }
}
