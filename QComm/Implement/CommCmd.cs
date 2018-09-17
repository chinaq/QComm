using CsvHelper.Configuration;
using QData.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QComm
{
    public class CommCmd
    {
        public CmdType CmdType { get; set; }
        public byte[] Sent { get; set; }
    }

    public enum CmdType {
        request,
        response
    }



    public sealed class CommCmdMap:ClassMap<CommCmd>
    {
        public CommCmdMap()
        {
            Map(m => m.Sent).ConvertUsing(row =>
            {
                var data = row.GetField<string>(nameof(CommCmd.Sent));
                return Datas.GetBytes(data);
            });
        }
    }


    public class Datas
    {
        public static byte[] GetBytes(string hex)
        {
            Regex regex = new Regex(@"\[(([0-9a-fA-F]){2}(\ ([0-9a-fA-F]){2})+)\]");
            Match match = regex.Match(hex);
            Capture capture = match.Groups[1];
            return Conv.StrHexToBytes(capture.Value);
        }
    }
}
