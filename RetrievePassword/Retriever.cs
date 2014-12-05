using System;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace RetrievePassword
{
    public class Retriever : IRetriever
    {
        private IDate _date;
        public virtual IDate Date
        {
            get
            {
                if (_date == null)
                {
                    _date = new Date();
                }
                return _date;
            }
        }

        public string Generate(string password, out double seconds, string strategy = null)
        {
            seconds = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            if (strategy == null)
            {
                return Md5(password + seconds);
            }
            return Md5(password + seconds + strategy);
        }

        public bool IsValid(string password, double seconds, string result, TimeSpan timeSpan, string strategy = null)
        {
            var time = new DateTime(1970, 1, 1).AddSeconds(seconds);
            if (Date.Now < time
                || Date.Now > time + timeSpan)
            {
                return false;
            }
            if (strategy == null)
            {
                return Md5(password + seconds) == result;
            }
            return Md5(password + seconds + strategy) == result;
        }

        private static string Md5(string password)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
        }
    }
}