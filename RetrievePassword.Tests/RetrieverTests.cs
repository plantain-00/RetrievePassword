using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace RetrievePassword.Tests
{
    [TestClass]
    public class RetrieverTests
    {
        public const string EMAIL = "test@gmail.com";
        private const string PASSWORD = "abc";
        public string Result;
        public double Seconds;
        private Mock<Retriever> _retriever;

        [TestInitialize]
        public void Init()
        {
            _retriever = new Mock<Retriever>();
            Result = _retriever.Object.Generate(PASSWORD, out Seconds);
        }

        [TestMethod]
        public void RightUser_RightTime()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(5));
            Assert.IsTrue(_retriever.Object.IsValid(PASSWORD, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void RightUser_TooEarly()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(-5));
            Assert.IsFalse(_retriever.Object.IsValid(PASSWORD, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void RightUser_TooLate()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(15));
            Assert.IsFalse(_retriever.Object.IsValid(PASSWORD, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void WrongUser_RightTime()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(5));
            Assert.IsFalse(_retriever.Object.IsValid("ddd", Seconds, Result, new TimeSpan(0, 10, 0)));
        }
    }
}