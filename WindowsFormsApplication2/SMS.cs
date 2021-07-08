using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class SMS
    {
        public string SendSms(string phoneNumber, string msg)
        {
            string xmlReturn=null;
            try
            {
                var client = new RestClient("http://10.129.52.103/oasms/service.asmx");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml;charset=utf-8");
                request.AddHeader("SOAPAction", "\"http://tempuri.org/SubmitSMS\"");
                request.AddParameter("text/xml;charset=utf-8", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Body>\r\n    <SubmitSMS xmlns=\"http://tempuri.org/\">\r\n      <ftelno>" + phoneNumber + "</ftelno>\r\n      <fmsg>" + msg + "</fmsg>\r\n      <fpwd>xxzx@2019</fpwd>\r\n      <forgaddr></forgaddr>\r\n    </SubmitSMS>\r\n  </soap:Body>\r\n</soap:Envelope>", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                xmlReturn = response.Content;
            }
            catch {
                xmlReturn = null;
            }
            
            InsterMysql d = new InsterMysql();
            if (xmlReturn.Contains("<SubmitSMSResult>ok</SubmitSMSResult>"))
            {
                d.insterInfoToMysqlSmsLog(msg, phoneNumber, "成功");
            }
            else {
                d.insterInfoToMysqlSmsLog(msg, phoneNumber, "失败");
            }
            
            return xmlReturn;
        }

    }
}
