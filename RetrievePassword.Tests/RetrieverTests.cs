using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Ploeh.AutoFixture;

namespace RetrievePassword.Tests
{
    [TestClass]
    public class RetrieverTests
    {
        public static string Email ;
        private static string password;
        public string Result;
        public string Seconds;
        private Mock<Retriever> _retriever;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            Email = fixture.Create<string>();
            password = fixture.Create<string>();
            _retriever = new Mock<Retriever>();
            Result = _retriever.Object.Generate(password, out Seconds);
        }

        [TestMethod]
        public void RightUser_RightTime()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(5));
            Assert.IsTrue(_retriever.Object.IsValid(password, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void RightUser_TooEarly()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(-5));
            Assert.IsFalse(_retriever.Object.IsValid(password, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void RightUser_TooLate()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(15));
            Assert.IsFalse(_retriever.Object.IsValid(password, Seconds, Result, new TimeSpan(0, 10, 0)));
        }

        [TestMethod]
        public void WrongUser_RightTime()
        {
            _retriever.Setup(r => r.Date.Now).Returns(DateTime.Now.AddMinutes(5));
            Assert.IsFalse(_retriever.Object.IsValid("ddd", Seconds, Result, new TimeSpan(0, 10, 0)));
        }
    }
}