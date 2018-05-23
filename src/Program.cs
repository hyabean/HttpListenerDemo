using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QLES.RestListenerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            string[] prefixes = new string[] { "http://localhost:8085/" };
            //listener.Prefixes = "";

            foreach (var item in prefixes)
            {
                listener.Prefixes.Add(item);
            }

            listener.Start();

            //Action A = null;
            //A.BeginInvoke();

            listener.BeginGetContext(GetContextCallback, listener);

            Console.ReadLine();
        }

        public static void GetContextCallback(IAsyncResult ar)
        {
            HttpListener listener = ar.AsyncState as HttpListener;

            var context = listener.EndGetContext(ar);

            Task.Factory.StartNew(() => { ProcessContext(context); });

            listener.BeginGetContext(GetContextCallback, listener);
        }

        private static void ProcessContext(HttpListenerContext context)
        {
            //if (context.Request.RawUrl.Contains("/api/UpdateTimeStragey"))
            if (context.Request.RawUrl.Contains(""))
            {
                var headers = context.Request.Headers;
                var length = context.Request.ContentLength64;
                byte[] contextBuffer = new byte[length];
                var contentLength = context.Request.InputStream.Read(contextBuffer, 0, (int)length);

                var contextStr = context.Request.ContentEncoding.GetString(contextBuffer);

                Console.WriteLine("-------------------------------------");

                Console.WriteLine(context.Request.RawUrl);
                foreach (var key in context.Request.Headers.AllKeys)
                {
                    Console.WriteLine("{0}:{1}", key, context.Request.Headers[key]);
                }
                Console.WriteLine("");
                Console.WriteLine(contextStr);

                Console.WriteLine("-------------------------------------");
            }
        }
    }
}
