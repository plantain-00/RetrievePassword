#define Example1
using System;
using System.Web;

namespace RetrievePassword.QuickStarted
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var e = "test@gmail.com";
            var p = "abc";
#if Example1
            var retriever = new Retriever();
            string t;
            var s = retriever.Generate(p, out t);

            var url = string.Format("http://localhost?s={0}&t={1}&e={2}", s, t, HttpUtility.UrlEncode(e));
            Console.WriteLine(url);

            Console.WriteLine(retriever.IsValid(p, t, s, new TimeSpan(0, 0, 10)));
#endif

#if Example2
            var retriever = new Retriever();
            string t;
            var strategy = Strategy.In10Seconds.ToString();
            var s = retriever.Generate(p, out t, strategy);

            var url = string.Format("http://localhost?s={0}&t={1}&e={2}&g={3}", s, t, HttpUtility.UrlEncode(e), HttpUtility.UrlEncode(strategy));
            Console.WriteLine(url);

            Console.WriteLine(retriever.IsValid(p, t, s, new TimeSpan(0, 0, 10), strategy));
#endif
            Console.Read();
        }
    }

    public enum Strategy
    {
        Forerver,
        In10Seconds
    }
}