using System;

namespace RetrievePassword
{
    public interface IRetriever
    {
        IDate Date { get; }
        string Generate(string password, out double seconds);
        bool IsValid(string password, double seconds, string result, TimeSpan timeSpan);
    }
}