using Microsoft.VisualStudio.TestTools.UnitTesting;
using QComm.UnitTest.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace QComm.UnitTest
{
    [TestClass]
    public class SerialClientTest
    {
        [TestMethod]
        public void Rev_out_of_time()
        {
            IClient cli = new SerialClient(COM.Port101);

            cli.Open();
            cli.ReadTimeOut = 10;
            byte[] rev = cli.Rev();
            cli.Close();

            Assert.AreEqual(0, rev.Length);
        }
    }
}
