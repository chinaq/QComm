using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QComm
{
    public class CsvConverter
    {
        public List<CommCmd> GetCommCmds(string sets)
        {
            List<CommCmd> cmds;
            using (var sr = new StringReader(sets))
            {
                using (var read = new CsvReader(sr))
                {
                    read.Configuration.RegisterClassMap<CommCmdMap>();
                    cmds = read.GetRecords<CommCmd>().ToList();
                }
            }
            return cmds;
        }
    }
}
