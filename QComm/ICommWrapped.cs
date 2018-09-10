using System;
using System.Collections.Generic;
using System.Text;

namespace QComm
{
    public interface ICommWrapped
    {
        void Run();
        void Setup(string sets);
        void Stop();
        void Open();
        void Close();
    }
}
