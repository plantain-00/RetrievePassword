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

            var url = $"http://localhost?s={s}&t={t}&e={HttpUtility.UrlEncode(e)}";
            Console.WriteLine(url);

            Console.WriteLine(retriever.IsValid(p, t, s, new TimeSpan(0, 0, 10)));
#endif

#if Example2
            var retriever = new Retriever();
            string t;
            var strategy = Strategy.In10Seconds.ToString();
            var s = retriever.Generate(p, out t, strategy);

            var url = $"http://localhost?s={s}&t={t}&e={HttpUtility.UrlEncode(e)}&g={HttpUtility.UrlEncode(strategy)}";
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