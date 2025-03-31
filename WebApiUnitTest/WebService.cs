using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUnitTest
{
    public interface IWebService
    {
        string CallApi(string uri, string requestMethod, string contentType, Dictionary<string, string> headers, string accept, string json);
    }

    public class WebService : IWebService
    {        
        public WebService()
        {

        }
        public string CallApi(string uri, string requestMethod, string contentType, Dictionary<string, string> headers, string accept, string json)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string returnResponse;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 30000;
            request.ContentType = contentType;
            if (!string.IsNullOrEmpty(accept))
            {
                request.Accept = accept;
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (requestMethod.ToUpper() == "GET")
            {
                request.Method = "GET";
            }
            else
            {
                request.Method = "POST";
                using (var dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(json);
                }
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream webStream = response.GetResponseStream())
                    {
                        if (webStream == null) return null;
                        using (var sr = new StreamReader(webStream))
                        {
                            returnResponse = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw;
            }
            return returnResponse;
        }

    }

    public static class ExtensionAsync
    {
        public static Task<Stream> GetRequestStreamAsync(this WebRequest request)
        {
            return Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, null);
        }
        public static Task<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Task<WebResponse>.Factory.FromAsync(
                request.BeginGetResponse, request.EndGetResponse, null);
        }
    }

}
