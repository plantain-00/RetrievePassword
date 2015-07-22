using System;

namespace RetrievePassword
{
    public class Date : IDate
    {
        public DateTime Now => DateTime.Now;
    }
}