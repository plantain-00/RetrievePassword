using System;

namespace RetrievePassword
{
    public interface IRetriever
    {
        IDate Date { get; }
        string Generate(string password, out string seconds, string strategy = null);
        bool IsValid(string password, string seconds, string result, TimeSpan timeSpan, string strategy = null);
    }
}