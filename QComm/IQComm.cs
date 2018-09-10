using System;
using System.Collections.Generic;
using System.Text;

namespace QComm
{
    public interface IQComm
    {
        void Response(int duration);
        void SetupResponse(string condition, string response, int waiting);
    }
}
