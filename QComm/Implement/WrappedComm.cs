using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using QData.Core;

namespace QComm
{
    public class WrappedComm : IComm
    {
        private ISetup _setup;
        private IClient _cli;
        private CsvConverter _convert;
        private Action<string> _callback;
        List<CommCmd> cmds;

        private WrappedComm()
        {
            _setup = new Mock<ISetup>().Object;
            _convert = new CsvConverter();
        }

        public WrappedComm(IClient cli, Action<string> callback = null): this()
        {
            _cli = cli;
            _callback = callback;
            if (_callback == null)
                _callback = str => {};
        }

        // public SerialComm(ISetup setup, IClient cli)
        // {
        //     _setup = setup;
        //     _cli = cli;
        //     _convert = new CsvConverter();
        // }

        public void Close()
        {
            _cli.Close();
            _callback("close");
        }

        public void Open()
        {
            _cli.Open();
            _callback("open");
        }

        public void Run()
        {
            _callback("run");
            foreach (var cmd in cmds)
            {
                byte[] rev = _cli.Rev();
                _callback("rev: " + Conv.BytesToStrHex(rev));
                _cli.Send(cmd.Sent);
                _callback("send: " + Conv.BytesToStrHex(cmd.Sent));
            }
        }

        public void Setup(string sets)
        {
            cmds = _convert.GetCommCmds(sets);
        }

        public void Stop()
        {
            _cli.Stop();
            _callback("stop");
        }
    }
}
