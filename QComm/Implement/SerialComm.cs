using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using QDatas.Core;

namespace QComm
{
    public class SerialComm : IComm
    {
        private ISetup _setup;
        private IClient _cli;
        private CsvConverter _convert;
        private Action<string> _callback;
        List<CommCmd> cmds;

        private SerialComm(string port)
        {
            _setup = new Mock<ISetup>().Object;
            _cli = new SerialClient(port);
            _convert = new CsvConverter();
        }

        public SerialComm(string port, Action<string> callback = null): this(port)
        {
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
                _callback("rev: " + QData.BytesToStrHex(rev));
                _cli.Send(cmd.Sent);
                _callback("send: " + QData.BytesToStrHex(cmd.Sent));
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
