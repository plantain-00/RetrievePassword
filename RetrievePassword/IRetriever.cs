using System;

namespace RetrievePassword
{
    public interface IRetriever
    {
        IDate Date { get; }
        string Generate(string password, out double seconds, string strategy = null);
        bool IsValid(string password, double seconds, string result, TimeSpan timeSpan, string strategy = null);
    }
}