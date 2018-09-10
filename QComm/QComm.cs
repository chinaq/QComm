using Moq;
using QDatas.Core;
using System;

namespace QComm
{
    public class QComm : IQComm
    {
        private Mock<ISetup> _setup;
        private IClient _client;

        public QComm(IClient client)
        {
            _setup = new Mock<ISetup>();
            _client = client;
        }


        public void Response(int duration)
        {
            _client.ReadTimeOut = duration;
            byte[] rev = _client.Rev();
            _setup.Object.Deal(QData.BytesToStrHex(rev));
        }

        public void SetupResponse(string condition, string response, int waiting)
        {
            _setup.Setup(s => s.Deal(condition)).Callback(() => _client.Send(QData.StrHexToBytes(response)));
        }
    }
}
