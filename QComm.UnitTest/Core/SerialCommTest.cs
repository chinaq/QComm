using Microsoft.VisualStudio.TestTools.UnitTesting;
using QComm.UnitTest.Core;
using QDatas.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QComm.UnitTest
{
    [TestClass]
    public class SerialCommTest
    {
        [TestMethod]
        public void Run()
        {
            ISetup set = new QSetup();
            IClient revCli = new SerialClient(COM.Port101);
            IComm serialComm = new SerialComm(set, revCli);

            string sets = @"CmdType,Sent
response,[12 34 56]
response,[34 56 78]";

            serialComm.Setup(sets);
            serialComm.Open();
            Task.Run(() => serialComm.Run());

            IClient client = new SerialClient(COM.Port102);
            client.Open();
            client.Send(QData.StrHexToBytes("12 34"));
            byte[] rev0 = client.Rev();
            client.Send(QData.StrHexToBytes("45 67"));
            byte[] rev1 = client.Rev();
            client.Send(QData.StrHexToBytes("78 90"));
            client.ReadTimeOut = 10;
            byte[] rev2 = client.Rev();
            client.Close();

            serialComm.Stop();
            serialComm.Close();
            Assert.AreEqual("12 34 56", QData.BytesToStrHex(rev0));
            Assert.AreEqual("34 56 78", QData.BytesToStrHex(rev1));
            Assert.AreEqual(0, rev2.Length);
        }


        [TestMethod]
        public void InitByPort() {
            IComm serialComm = new SerialComm(COM.Port101);
            string sets = @"CmdType,Sent
response,[34 56 78]";

            serialComm.Setup(sets);
            serialComm.Open();
            Task.Run(() => serialComm.Run());

            IClient client = new SerialClient(COM.Port102);
            client.Open();
            client.Send(QData.StrHexToBytes("12 34"));
            byte[] rev0 = client.Rev();
            client.Close();

            serialComm.Stop();
            serialComm.Close();
            Assert.AreEqual("34 56 78", QData.BytesToStrHex(rev0));
        }
    }
}
