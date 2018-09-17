using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QData.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QComm.UnitTest
{
    [TestClass]
    public class CsvConverterTest
    {
        [TestMethod]
        public void Convert()
        {
            string sets = @"CmdType,Sent
response,[12 34 56]
response,[34 56 78]";

            //using (var sr = new StringReader(sets))
            //{
            //    using (var read = new CsvReader(sr))
            //    {
            //        read.Configuration.RegisterClassMap<CommCmdMap>();
            //        var commands = read.GetRecords<CommCmd>().ToList();

            //        Assert.AreEqual(2, commands.Count);
            //        CollectionAssert.AreEqual(Conv.StrHexToBytes("12 34 56"), commands[0].Sent);
            //    }
            //}

            var conv = new CsvConverter();
            var commands = conv.GetCommCmds(sets);
            Assert.AreEqual(2, commands.Count);
            CollectionAssert.AreEqual(Conv.StrHexToBytes("12 34 56"), commands[0].Sent);


        }
    }
}
