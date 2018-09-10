using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QComm.UnitTest
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void MatchHex()
        {
            Regex regex = new Regex(@"\[(([0-9a-fA-F]){2}(\ ([0-9a-fA-F]){2})+)\]");
            Match match = regex.Match("[12 34 Ee]");

            Assert.IsTrue(match.Success);

            Capture capture = match.Groups[1];
            Assert.AreEqual("12 34 Ee", capture.Value);
        }
    }
}
