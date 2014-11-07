using System;
using System.Security.Cryptography;
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

        public string Generate(string password, out double seconds)
        {
            seconds = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            return Md5(password + seconds);
        }

        public bool IsValid(string password, double seconds, string result, TimeSpan timeSpan)
        {
            var time = new DateTime(1970, 1, 1).AddSeconds(seconds);
            if (Date.Now < time
                || Date.Now > time + timeSpan)
            {
                return false;
            }
            return Md5(password + seconds) == result;
        }

        private static string Md5(string password)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
        }
    }
}