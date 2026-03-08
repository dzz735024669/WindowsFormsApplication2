using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class TencentVmsInterface
    {
        public class CallBackResult
        {

            public int result { get; set; }
            public string errmsg { get; set; }
            public string callid { get; set; }
            public string ext { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone">tel 的 mobile 字段的内容</param>
        /// <param name="strrand">URL 中的 random 字段的值</param>
        /// <param name="strtime">UNIX 时间戳</param>
        /// <returns></returns>
        public string GetSig(string phone, string strrand, string strtime)
        {
            string strMobile = phone; //tel 的 mobile 字段的内容
            string strAppKey = "10be0fdf12b0bde0f88c58d640e3d7e0"; //sdkappid 对应的 appkey，需要业务方高度保密
            string strRand = strrand; //URL 中的 random 字段的值
            string strTime = strtime; //UNIX 时间戳
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            strTime = unixTimestamp.ToString();
            string sig = GetSHA256HashFromString("appkey=" + strAppKey + "&random=" + strRand + "&time=" + strTime + "&mobile=" + strMobile + "");
            return sig;
        }
        public string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }
        /// <summary>
        /// 语音接口
        /// </summary>
        /// <param name="sdkappid">应用后生成的实际 SDK AppID</param>
        /// <param name="random">随机数字</param>
        /// <param name="tpl_id">模板 ID</param>
        /// <param name="Params">模板参数，若模板没有参数，请提供为空数组若使用数字则默认按照个十百千万进行播报，可通过在数字前添加英文逗号（,）改变播报方式，例如5,6,7,8</param>
        /// <param name="playtimes">播放次数，可选，最多3次，默认2次</param>
        /// <param name="sig">App 凭证</param>
        /// <param name="mobile">电话号码</param>
        /// <param name="time">请求发起时间，UNIX 时间戳</param>
        public CallBackResult CallPhone(string sdkappid, string random, string tpl_id, string Params, string playtimes, string mobile)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string strTime = unixTimestamp.ToString();//unix时间戳
            string sig = GetSig(mobile, random, strTime);
            var client = new RestClient("https://cloud.tim.qq.com/v5/tlsvoicesvr/sendtvoice?sdkappid=" + sdkappid + "&random=" + random + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"tpl_id\": " + tpl_id + ",\r\n  \"params\": [ " + Params + " ],\r\n  \"playtimes\": " + playtimes + ",\r\n  \"sig\": \"" + sig + "\",\r\n  \"tel\": {\r\n      \"mobile\": \"" + mobile + "\",\r\n      \"nationcode\": \"86\"\r\n  },\r\n  \"time\": " + strTime + ",\r\n  \"ext\": \"\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            CallBackResult zb = JsonConvert.DeserializeObject<CallBackResult>(response.Content);
            insterInfoToMysql(zb, Params, mobile);
            return zb;

        }
        public void insterInfoToMysql(CallBackResult zb, string Params, string mobile) {
            String connetStr = "server=10.129.27.1;port=3306;user=*;password=*; database=Monitor;";
          
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                //在这里使用代码对数据库进行增删查改
                string insertIntoStr = @"INSERT INTO `Monitor`.`VMS_LOG` (`callid`, `errmsg`, `mobile`, `message`, `datetime`, `callResult`, `keypress`, `CallBackResult`, `accept_time`) VALUES ('"+zb.callid+"', '"+zb.errmsg+"', '"+mobile+"', '"+Params+"', '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', '', '', '', '');";
                MySqlCommand cmd = new MySqlCommand(insertIntoStr, conn);
                int result = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
               // Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

