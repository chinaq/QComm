using System;
using System.Collections.Generic;
using System.Text;

namespace QComm
{
    public class SerialComm : ICommWrapped
    {
        private ISetup _setup;
        private IClient _cli;
        private CsvConverter _convert;
        List<CommCmd> cmds;

        public SerialComm(ISetup setup, IClient cli)
        {
            _setup = setup;
            _cli = cli;
            _convert = new CsvConverter();
        }

        public void Close()
        {
            _cli.Close();
        }

        public void Open()
        {
            _cli.Open();
        }

        public void Run()
        {
            foreach (var cmd in cmds)
            {
                byte[] rev = _cli.Rev();
                _cli.Send(cmd.Sent);
            }
        }

        public void Setup(string sets)
        {
            cmds = _convert.GetCommCmds(sets);
        }

        public void Stop()
        {
            _cli.Stop();
        }
    }
}
