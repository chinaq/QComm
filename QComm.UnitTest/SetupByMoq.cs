using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QDatas.Core;
using System.Text.RegularExpressions;

namespace QComm.UnitTest
{
    [TestClass]
    public class SetupByMoq
    {
        [TestMethod]
        public void SetUp()
        {
            Mock<ITestComm> qcomm = new Mock<ITestComm>();
            qcomm
                .Setup(x => x.Response(It.IsRegex("abc", RegexOptions.IgnoreCase)))
                .Returns("aoo");
            qcomm
                .Setup(x => x.Response(It.IsRegex("def", RegexOptions.IgnoreCase)))
                .Returns("doo");

            Assert.AreEqual("aoo", qcomm.Object.Response("abc"));
            Assert.AreEqual("doo", qcomm.Object.Response("def"));
        }


        #region response

        [TestMethod]
        public void NoResponse()
        {
            var client = new Mock<IClient>();
            client.Setup(c => c.Rev()).Returns(QData.StrHexToBytes("aa bb"));

            IQComm comm = new QComm(client.Object);
            var condition = "123";
            var response = "456";
            var waiting = 100;
            var duration = 200;

            comm.SetupResponse(condition, response, waiting);
            comm.Response(duration);

            client.Verify(c => c.Send(It.IsAny<byte[]>()), Times.Never); 
        }


        [TestMethod]
        public void Response()
        {
            var client = new Mock<IClient>();
            client.Setup(c => c.Rev()).Returns(QData.StrHexToBytes("12 34"));

            IQComm comm = new QComm(client.Object);
            var condition = "12 34";
            var response = "45 67";
            var waiting = 100;
            var duration = 200;

            comm.SetupResponse(condition, response, waiting);
            comm.Response(duration);

            var back = QData.StrHexToBytes("45 67");
            client.Verify(c => c.Send(back));
        }

        #endregion

    }
}
