using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using NLog;

namespace ArganaWeedClietApi
{
    public class Session
    {
        private static readonly Lazy<HttpClient> Lazy = new Lazy<HttpClient>(() => new HttpClient { Timeout = TimeSpan.FromSeconds(5) , BaseAddress= new Uri("http://localhost:5153/") });

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static HttpClient HttpClient => Lazy.Value;

        const string HTTP_CONTENT_BYTE = "application/octet-stream";
        const string HTTP_CONTENT_JSON = "application/json";

        public Session()
        {

        }

        public async Task<byte[]> TransmitPost(string target, byte[] data)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            HttpContent content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(HTTP_CONTENT_BYTE);
            HttpResponseMessage response = await HttpClient.PostAsync(target, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(" Server Response status Code: {0} - Reason: {0}", response.StatusCode, response.ReasonPhrase));
            }

            watch.Stop();

            var result = await response.Content.ReadAsByteArrayAsync();

            Logger.Info("target {0} - exec time {1}", target, watch.ElapsedMilliseconds);

            return result;
        }


        public async Task<string> TransmitPostJsonAsync(string target, string data)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            StringContent sc = new StringContent(data, Encoding.UTF8, HTTP_CONTENT_JSON);
            HttpResponseMessage response = await HttpClient.PostAsync(target, sc);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(" Server Response status Code: {0} - Reason: {0}", response.StatusCode, response.ReasonPhrase));
            }

            watch.Stop();

            var result = await response.Content.ReadAsStringAsync();

            Logger.Info("target {0} - exec time {1}", target, watch.ElapsedMilliseconds);

            return result;
        }
        
        public async Task<string> TransmitGetJsonAsync(string target)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //StringContent sc = new StringContent(data, Encoding.UTF8, HTTP_CONTENT_JSON);
            HttpResponseMessage response = await HttpClient.GetAsync(target);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(" Server Response status Code: {0} - Reason: {0}", response.StatusCode, response.ReasonPhrase));
            }

            watch.Stop();

            var result = await response.Content.ReadAsStringAsync();

            Logger.Info("target {0} - exec time {1}", target, watch.ElapsedMilliseconds);

            return result;
        }


        /*public async Task<Image> TransmitGetImage(Uri target, string data)
        {
            StringContent sc = new StringContent(data, Encoding.UTF8, HTTP_CONTENT_JSON);
            HttpResponseMessage response = await HttpClient.PostAsync(target, sc);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(" Server Response status Code: {0} - Reason: {0}", response.StatusCode, response.ReasonPhrase));
            }

            Stream stream = await response.Content.ReadAsStreamAsync();

            return Image.FromStream(stream);
        }*/
    }
}
